using EduZone.Courses;
using EduZone.Enum;
using EduZone.Instructors;
using EduZone.Notifications;
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
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace EduZone.Enrollments
{
    [Authorize]
    public class EnrollmentAppService : EduZoneAppService, IEnrollmentAppService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IGetUserNameFromToken _getUserNameFromToken;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly INotificationInstructorAppService _notificationInstructorAppService;
        private readonly IDataFilter _dataFilter;

        public EnrollmentAppService(IEnrollmentRepository enrollmentRepository, 
            IGetUserNameFromToken getUserNameFromToken, IStudentRepository studentRepository, 
            INotificationInstructorAppService notificationInstructorAppService, 
            ICourseRepository courseRepository, IDataFilter dataFilter)
        {
            _enrollmentRepository = enrollmentRepository;
            _getUserNameFromToken = getUserNameFromToken;
            _studentRepository = studentRepository;
            _notificationInstructorAppService = notificationInstructorAppService;
            _courseRepository = courseRepository;
            _dataFilter = dataFilter;
        }

        public async Task<EnrollmentDto> AddNewEnroll(NewEnrollmentInput input)
        {
            var studentEmail = _getUserNameFromToken.GetEmailFromToken();
            var student = await _studentRepository.GetStudentByEmail(studentEmail);

            // check if student already in course:
            bool isExist = (await _enrollmentRepository.GetListAsyncWithoutTenant()).Any(
                e => e.CourseId == input.CourseId && e.StudentId == student.Id);

            if (isExist)
            {
                throw new UserFriendlyException(L[EduZoneDomainErrorCodes.alreadyEnrolled]);
            };

            using (_dataFilter.Disable<IMultiTenant>())
            {

                var course = await _courseRepository.GetCourseById(input.CourseId);
                var newEnroll = new Enrollment(GuidGenerator.Create(), student.Id, input.CourseId, false,
                    ServiceHelper.getTimeSpam(DateTime.UtcNow)!.Value, null, CourseStatus.Enrolled);
                await _enrollmentRepository.InsertAsync(newEnroll, true);


                // send notification to instructor:
                try
                {
                    await SendNotificationForProvider(student.Id, course.InstructorId, newEnroll.Id, course.Title, student.FirstName + " " + student.LastName);
                }
                catch { }


                return ObjectMapper.Map<Enrollment, EnrollmentDto>(newEnroll);
            }
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


        #region methods

        private async Task SendNotificationForProvider(Guid studentId, Guid instructorId, Guid enrollId,string courseName, string studentName)
        {
            await _notificationInstructorAppService.CreateNewEnrollmentNotification(studentId, instructorId, enrollId,courseName,
                "newStudentAddedYourCourse", NotificationTypeEnum.NewEnrollment,
                new Dictionary<string, string> { { "studentName", studentName },{ "courseName", courseName } });
        }

        #endregion
    }
}
