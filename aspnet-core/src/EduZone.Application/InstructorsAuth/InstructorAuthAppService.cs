using EduZone.DataSeeder;
using EduZone.Emailing;
using EduZone.Instructors;
using EduZone.Licenses;
using EduZone.Permissions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace EduZone.InstructorsAuth
{
    public class InstructorAuthAppService : EduZoneAppService, IInstructorsAuthAppService
    {
        private readonly IdentityUserManager _userManager;
        private readonly IdentityRoleManager _roleManager;

        private readonly IPermissionManager _permissionManager;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        private readonly ITenantManager _tenantManager;
        private readonly ITenantRepository _tenantRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        private readonly IConfiguration _configuration;
        private readonly IBackgroundJobManager _backgroundJobManager;

        private readonly IInstructorRepository _instructorRepository;

        private IRepository<License> _licenseRepository;

        public InstructorAuthAppService(IdentityUserManager userManager, IdentityRoleManager roleManager, 
            IPermissionManager permissionManager, IPermissionDefinitionManager permissionDefinitionManager,
            ITenantManager tenantManager, ITenantRepository tenantRepository, IDistributedEventBus distributedEventBus, 
            IConfiguration configuration, IBackgroundJobManager backgroundJobManager, 
            IInstructorRepository instructorRepository, IRepository<License> licenseRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _permissionDefinitionManager = permissionDefinitionManager;
            _tenantManager = tenantManager;
            _tenantRepository = tenantRepository;
            _distributedEventBus = distributedEventBus;
            _configuration = configuration;
            _backgroundJobManager = backgroundJobManager;
            _instructorRepository = instructorRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task<bool> CreateNewInstructor(NewInstructorInput input)
        {
            var isUnique = await _checkUniqueEmail(input.Email);
            if (isUnique == 0) throw new UserFriendlyException(L[EduZoneDomainErrorCodes.EmailShouldBeUniqueMessage]);

            var newTenantName = await _normalizeTenantName(input.FirstName + " " + input.LastName);
            
            // check license: 

            bool isValidLicense = await _isLicenseValid(input.License);

            if (!isValidLicense) throw new UserFriendlyException(L[EduZoneDomainErrorCodes.InvalidLicense]);

            var license = await _licenseRepository.FirstOrDefaultAsync(l => l.Key == input.License)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.InvalidLicense]);

            // end check license...


            #region create new tenant
            var createdTenant = await _tenantManager.CreateAsync(newTenantName);
            await _tenantRepository.InsertAsync(createdTenant, true);

            await _distributedEventBus.PublishAsync(new TenantCreatedEto
            {
                Id = createdTenant.Id,
                Name = createdTenant.Name,
                Properties =
                {
                    {"AdminEmail","admin@abp.io" },
                    {"AdminPassword","1q2w3E*" }
                }
            });

            await _createTenantRoles(createdTenant.Id);
            #endregion

            #region create new user
            // Create new user in users table:
            var newUser = new IdentityUser(new Guid(), input.Email, input.Email, createdTenant.Id);
            newUser.SetIsActive(false);
            var result = await _userManager.CreateAsync(newUser, input.Password);

            // Assign role to use:
            using (CurrentTenant.Change(createdTenant.Id))
            {
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, EduZoneConsts.InstructorRoleName);
                }
            }
            #endregion

            var instructor = new Instructor(GuidGenerator.Create(),createdTenant.Id, input.FirstName, input.LastName, input.Gender, input.Email
                , input.About, license.Id);

            await _instructorRepository.InsertAsync(instructor, true);

            if (CurrentUnitOfWork != null) await CurrentUnitOfWork.CompleteAsync();

            // make licesnse as used:

            license.IsUsed = true;

            await _licenseRepository.UpdateAsync(license,true);

            // Send Email:

            await SendVerificationEmail(input.Email, input.FirstName + " " + input.LastName, createdTenant.Name);

            return true;
        }

        private async Task SendVerificationEmail(string email, string instructorName, string tenantName)
        {

            using (DataFilter.Disable<IMultiTenant>())
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new UserFriendlyException("NotFound");
                }

                string confimationLink = await _generateEmailConformaitionLink(user);

                try
                {
                    await _backgroundJobManager.EnqueueAsync(
                    new EmailSendingArgs
                    {
                        Template = "Verification",
                        TargetEmail = email,
                        ConfirmationLink = confimationLink,
                        EmailPlaceHolder = user.Email,
                        TenantPlaceHolder = tenantName,
                        InstructorNamePlaceHolder = instructorName
                    });
                }

                catch (Exception ex) { }
            }
        }

        private async Task SendWelcomeEmail(string email, Guid instructorId, string InstructorName, string tenantName)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                await _backgroundJobManager.EnqueueAsync(
                new EmailSendingArgs
                {
                    Template = "Welcome",
                    TargetEmail = email,
                    EmailPlaceHolder = email,
                    InstructorNamePlaceHolder = InstructorName,
                    TenantPlaceHolder = tenantName,
                    InstructerId = instructorId
                }
                );
            }

        }

        public async Task<bool> Verify(VerifyCodeDto input)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                IdentityUser? user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    input.Token = input.Token.Replace(" ", "+");
                    var result = await _userManager.ConfirmEmailAsync(user, input.Token);
                    if (result.Succeeded)
                    {
                        user.SetIsActive(true);
                        await _userManager.UpdateAsync(user);
                        var instructor = ObjectMapper.Map<Instructor, InstructorDto>(await _instructorRepository.GetInstructorByEmail(user.Email));
                        var instructorName = instructor.FirstName + " " + instructor.LastName;
                        var tenantName = await _tenantRepository.GetAsync(instructor.TenantId);
                        await SendWelcomeEmail(user.Email, instructor.Id, instructorName, tenantName.Name);
                        return true;
                    }
                    else if (result.Errors.Any(x => x.Code == "InvalidToken")){
                        return false;
                    }
                }
                return false;
            }
        }

        public async Task ResendVerficationEmail(string targetEmail)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                //find user by email
                var user = await _userManager.FindByEmailAsync(targetEmail.Replace(" ", "+"));
                if (user == null) throw new UserFriendlyException("Not Found");
                string confirmationLink = await _generateEmailConformaitionLink(user);
                await _backgroundJobManager.EnqueueAsync(
                new EmailSendingArgs
                {
                    Template = "Verification",
                    TargetEmail = user.Email,
                    ConfirmationLink = confirmationLink
                }
                );
            }
        }


        #region Methods

        private async Task<int> _checkUniqueEmail(string email)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null) { return 0; }
                else { return 1; }
            }
        }

        private async Task<string> _normalizeTenantName(string instructorName)
        {
            var newTenantName = instructorName;
            var tenant = await _tenantRepository.FindByNameAsync(newTenantName);
            if (tenant is not null)
            {
                var tenants = await _tenantRepository.GetListAsync();
                string pattern = @"" + instructorName + "[_]*\\d*";
                Regex rg = new Regex(pattern);
                var tenantWithTheSameStart = tenants.Where(tenant => rg.Matches(tenant.Name).Any());
                var lastTenant = tenantWithTheSameStart.Last();

                string patternNoTenantNameId = @"\A" + instructorName + "\\Z";
                Regex rgNoTenantNameId = new Regex(patternNoTenantNameId);
                string patternWithTenantNameId = @"" + instructorName + "[_]+\\d+";
                Regex rgWithTenantNameId = new Regex(patternNoTenantNameId);
                var tenantNameId = "";

                if (tenants.Where(tenant => rgWithTenantNameId.Matches(newTenantName).Any()).Count() > 0)
                {
                    tenantNameId = (rgNoTenantNameId.Matches(lastTenant.Name).Count()).ToString("00");
                }

                newTenantName = newTenantName + tenantNameId;

            }

            return newTenantName;
        }

        private async Task _createTenantRoles(Guid tenantId)
        {
            var permissions = await _permissionDefinitionManager.GetPermissionsAsync();
            using (CurrentTenant.Change(tenantId))
            {
                var adminRole = await _roleManager.FindByNameAsync("admin");
                string InstructorRoleName = EduZoneConsts.InstructorRoleName;
                var instructorRolePermission = permissions.Where(x => x.IsEnabled &&
                (x.Name.StartsWith(EduZonePermissions.GroupName))
                && x.MultiTenancySide != MultiTenancySides.Host);

                var createdRole = await _roleManager.CreateAsync(new IdentityRole(Guid.NewGuid(),
                    InstructorRoleName, tenantId));

                foreach (var permission in instructorRolePermission)
                {
                    try { await _permissionManager.SetForRoleAsync(InstructorRoleName, permission.Name, true); } catch { }
                }
            }
        }

        private async Task<string> _generateEmailConformaitionLink(IdentityUser user)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                try
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string confirmLink = _configuration["App:ClientUrl"] +
                        _configuration["App:ConfirmationEmailLink"] + user.Email + "&c=" + token;
                    return confirmLink;
                }
                catch (Exception ex) { }

                return "";
            }
        }

        private async Task<bool> _isLicenseValid(string licenseKey)
        {
            var license = await _licenseRepository.FirstOrDefaultAsync(l => l.Key == licenseKey);
            return license != null && license.ExpirationDate > DateTime.UtcNow && !license.IsUsed;
        }

        #endregion


    }
}
