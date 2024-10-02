using EduZone.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace EduZone.Students
{
    [RemoteService(IsEnabled = false)]
    public class StudentAppService : EduZoneAppService, IStudentAppService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentAppService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [Authorize(EduZonePermissions.Dashboard.Host)]
        public async Task<PagedResultDto<StudentDto>> GetAllStudent(GetStudentInput input)
        {
            var totalCount = await _studentRepository.GetCountAsync(input.FilterText, input.Gender);
            var items = await _studentRepository.GetListAsync(input.FilterText, input.Gender,
                input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<StudentDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Student>, List<StudentDto>>(items)
            };
        }

        [Authorize(EduZonePermissions.Dashboard.Host)]
        public async Task<StudentDto> GetStudentById(Guid id)
        {
            var student = await _studentRepository.GetAsync(id)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);
            return ObjectMapper.Map<Student, StudentDto>(student);
        }
    }
}
