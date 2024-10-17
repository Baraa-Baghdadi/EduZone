using EduZone.Permissions;
using EduZone.UserNameFromToken;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace EduZone.Instructors
{
    [RemoteService(IsEnabled = false)]
    public class InstructorsAppService : EduZoneAppService, IInstructorsAppService
    {

        private readonly IInstructorRepository _instructorRepository;
        private readonly IDataFilter _dataFilter;
        private readonly GetUserNameFromToken _getUserNameFromToken;

        public InstructorsAppService(
            IInstructorRepository instructorRepository, IDataFilter dataFilter,
            GetUserNameFromToken getUserNameFromToken)
        {
            _instructorRepository = instructorRepository;
            _dataFilter = dataFilter;
            _getUserNameFromToken = getUserNameFromToken;
        }

        [Authorize(EduZonePermissions.Dashboard.Host)]
        public async Task<PagedResultDto<InstructorDto>> GetAllInstructor(GetInstructorInput input)
        {
            using (_dataFilter.Disable<IMultiTenant>()) {
                var totalCount = await _instructorRepository.GetCountAsync(input.FilterText, input.Gender);
                var items = await _instructorRepository.GetListAsync(input.FilterText, input.Gender,
                    input.Sorting, input.MaxResultCount, input.SkipCount);
                return new PagedResultDto<InstructorDto>
                {
                    TotalCount = totalCount,
                    Items = ObjectMapper.Map<List<Instructor>, List<InstructorDto>>(items)
                };
            }
        }

        [Authorize(EduZonePermissions.Dashboard.Host)]
        public async Task<InstructorDto> GetInstructorById(Guid id)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var instructor = await _instructorRepository.GetAsync(id);
                return ObjectMapper.Map<Instructor, InstructorDto>(instructor);
            }
        }

        [Authorize(EduZonePermissions.Dashboard.Tenant)]
        public async Task<InstructorDto> GetInstructorInfo()
        {
            var tenantId = CurrentTenant.Id;
            var instructor = await _instructorRepository.FirstOrDefaultAsync(r => r.TenantId == tenantId)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.UserNotFound]);
            return ObjectMapper.Map<Instructor, InstructorDto>(instructor);
        }

        [Authorize(EduZonePermissions.Dashboard.Tenant)]
        public async Task<bool> UpdateInstructorInfo(UpdateInstructorInfoInput input)
        {
            var tenantId = CurrentTenant.Id;
            var instructor = await _instructorRepository.FirstOrDefaultAsync(r => r.TenantId == tenantId)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.UserNotFound]);

            // update Info:
            instructor.FirstName = input.FirstName;
            instructor.LastName = input.LastName;
            instructor.Gender = input.Gender;
            instructor.About = input.About;

            return true;
        }



        #region methods



        #endregion
    }
}
