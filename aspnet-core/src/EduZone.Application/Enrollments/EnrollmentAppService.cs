using EduZone.Enum;
using EduZone.Students;
using EduZone.UserNameFromToken;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EduZone.Enrollments
{
    [Authorize]
    public class EnrollmentAppService : EduZoneAppService, IEnrollmentAppService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IGetUserNameFromToken _getUserNameFromToken;
        private readonly IStudentRepository _studentRepository;

        public EnrollmentAppService(IEnrollmentRepository enrollmentRepository, IGetUserNameFromToken getUserNameFromToken, IStudentRepository studentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _getUserNameFromToken = getUserNameFromToken;
            _studentRepository = studentRepository;
        }

        public async Task<EnrollmentDto> AddNewEnroll(NewEnrollmentInput input)
        {
            var studentEmail = _getUserNameFromToken.GetEmailFromToken();
            var student = await _studentRepository.GetStudentByEmail(studentEmail);

            var newEnroll = new Enrollment(GuidGenerator.Create(), student.Id,input.CourseId,false,
                ServiceHelper.getTimeSpam(DateTime.UtcNow)!.Value,null,CourseStatus.Enrolled);
            await _enrollmentRepository.InsertAsync(newEnroll, true);


            return ObjectMapper.Map<Enrollment,EnrollmentDto>(newEnroll);
        }

        public async Task<PagedResultDto<EnrollmentDto>> GetEnrollmentsOfInstructor(GetEnrollmentInput input)
        {
            var totalCount = await _enrollmentRepository.GetCountAsync(input.FilterText);
            var items = await _enrollmentRepository.GetListAsync(input.FilterText,
                input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<EnrollmentDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Enrollment>, List<EnrollmentDto>>(items)
            };
        }

        public async Task<EnrollmentDto> GetEnrollmentById(Guid id) 
        {
            var enroll = await _enrollmentRepository.GetByIdWithDetails(id)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);
            return ObjectMapper.Map<Enrollment,EnrollmentDto>(enroll);
        }
    }
}
