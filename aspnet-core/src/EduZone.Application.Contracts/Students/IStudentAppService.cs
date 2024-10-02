using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EduZone.Students
{
    public interface IStudentAppService : IApplicationService
    {
        Task<PagedResultDto<StudentDto>> GetAllStudent(GetStudentInput input);
        Task<StudentDto> GetStudentById(Guid id);
    }
}
