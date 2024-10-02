using EduZone.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace EduZone.Instructors
{
    [RemoteService(IsEnabled = false)]
    public class InstructorsAppService : EduZoneAppService, IInstructorsAppService
    {

        private readonly IInstructorRepository _instructorRepository;
        private readonly IDataFilter _dataFilter;

        public InstructorsAppService(
            IInstructorRepository instructorRepository, IDataFilter dataFilter)
        {
            _instructorRepository = instructorRepository;
            _dataFilter = dataFilter;
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

        #region methods

     

        #endregion
    }
}
