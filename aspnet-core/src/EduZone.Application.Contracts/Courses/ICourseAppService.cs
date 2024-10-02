using EduZone.Enrollments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EduZone.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        Task<PagedResultDto<CourseDto>> GetMyCourses(GetCoursesInput input);
        Task<PagedResultDto<CourseDto>> GetAllCourses(GetCoursesInput input);
        Task<CourseDto> CreateNewCourse(NewCourseInput input);
        Task<CourseDto> GetCourseById(Guid id);
    }
}
