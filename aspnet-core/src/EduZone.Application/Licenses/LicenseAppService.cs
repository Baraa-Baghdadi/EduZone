using EduZone.Instructors;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace EduZone.Licenses
{
    [Authorize]
    public class LicenseAppService : EduZoneAppService, ILicenseAppService
    {
        private readonly ILicenseRepository _licenseRepository;

        public LicenseAppService(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public async Task<LicenseDto> GetById(Guid id)
        {
           var License = await _licenseRepository.GetLicenseAsync(id);
            return ObjectMapper.Map<License,LicenseDto>(License);
        }

        public async Task<PagedResultDto<LicenseDto>> GetListAsync(GetLicenseInput input)
        {
            var totalCount = await _licenseRepository.GetCountAsync(input.FilterText);
            var items = await _licenseRepository.GetListAsync(input.FilterText,
                input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<LicenseDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<License>, List<LicenseDto>>(items)
            };
        }
    }
}
