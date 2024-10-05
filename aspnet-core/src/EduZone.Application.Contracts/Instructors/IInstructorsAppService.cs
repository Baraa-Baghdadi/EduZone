using EduZone.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EduZone.Instructors
{
    public interface IInstructorsAppService : IApplicationService
    {
        Task<PagedResultDto<InstructorDto>> GetAllInstructor(GetInstructorInput input); // get all for admin
        Task<InstructorDto> GetInstructorById(Guid id); // get by Id
        Task<InstructorDto> GetInstructorInfo(); // get by token
        Task<bool> UpdateInstructorInfo(UpdateInstructorInfoInput input);
    }
}
