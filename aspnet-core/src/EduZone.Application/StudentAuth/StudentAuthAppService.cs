using EduZone.ApiResponse;
using EduZone.Enum;
using EduZone.Students;
using EduZone.UserNameFromToken;
using System.Globalization;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace EduZone.StudentAuth
{
    [RemoteService(IsEnabled = false)]
    public class StudentAuthAppService : EduZoneAppService, IStudentAuthAppService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IdentityUserManager _userManager;
        private readonly IGetUserNameFromToken _getUserNameFromToken;
        private readonly IApiResponse _apiResponse;

        public StudentAuthAppService(IStudentRepository studentRepo, IdentityUserManager userManager, IApiResponse apiResponse,
            IGetUserNameFromToken getUserNameFromToken)
        {
            _studentRepo = studentRepo;
            _userManager = userManager;
            _apiResponse = apiResponse;
            _getUserNameFromToken = getUserNameFromToken;
        }

        public async Task<Response<bool>> AddNewStudent(NewStudentInput input)
        {
            IdentityUser newUser;

            var isEmailExist = await _userManager.FindByEmailAsync(input.Email);
            if (isEmailExist is not null) throw new UserFriendlyException(L[EduZoneDomainErrorCodes.EmailalreadyExist]);

            var currentLanguageAsString = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var currentLanguage = currentLanguageAsString == "en" ? ApplicationLanguage.en : ApplicationLanguage.ar;

            var username = ServiceHelper.GetUsernameBeforeAt(input.Email);
            var newEmail = $"{input.Email}";
            var password = input.Password;

            // check password:
            var isPasswordComplex = ServiceHelper.IsPasswordComplex(password);
            if (!isPasswordComplex) throw new UserFriendlyException(L[EduZoneDomainErrorCodes.InvalidPassword]);


            // Generate unique custom student id:
            string customStudenttId = ServiceHelper._generateCustomStudentId();
            bool studentIdIsExist = await _studentRepo.AnyAsync(p => p.CustomStudenttId == customStudenttId);
            while (studentIdIsExist)
            {
                customStudenttId = ServiceHelper._generateCustomStudentId();
                studentIdIsExist = await _studentRepo.AnyAsync(p => p.CustomStudenttId == customStudenttId);
            }

            // create new student:
            var newStudent = new Student(GuidGenerator.Create(),customStudenttId,null,null,null,input.Email,null, currentLanguage);
            await _studentRepo.InsertAsync(newStudent,true);
         

            // cretae new user:
            newUser = new IdentityUser(GuidGenerator.Create(), username, newEmail,null);
            newUser.SetIsActive(true);
            newUser.SetEmailConfirmed(true);
            await _userManager.CreateAsync(newUser, password);
           
            
            return _apiResponse.Success(true);
        }

        public async Task<Response<bool>> UpdateStudentInfo(UpdateStudentInfo input)
        {
            var userEmail = _getUserNameFromToken.GetEmailFromToken();
            var student = await _studentRepo.GetStudentByEmail(userEmail) 
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.UserNotFound]);
            student.FirstName = input.FirstName;
            student.LastName = input.LastName;
            student.Gender = input.Gender;
            student.DOB = input.DOB;
            await _studentRepo.UpdateAsync(student,true);

            return _apiResponse.Success(true);

        }
    }
}
