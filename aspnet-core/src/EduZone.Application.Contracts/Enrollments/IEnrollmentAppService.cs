using EduZone.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EduZone.Enrollments
{
    public interface IEnrollmentAppService : IApplicationService
    {
        Task<PagedResultDto<EnrollmentDto>> GetEnrollmentsOfInstructor(GetEnrollmentInput input);
        Task<EnrollmentDto> GetEnrollmentById(Guid id);
        Task<bool> AddNewEnroll(NewEnrollmentInput input);
    }
}
