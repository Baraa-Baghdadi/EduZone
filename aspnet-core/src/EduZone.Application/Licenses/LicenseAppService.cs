using EduZone.Instructors;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
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

        public async Task<bool> CreateLicanse(CreateLicenseInput input)
        {
            bool isExist = await _licenseRepository.AnyAsync(l => l.Key == input.Key);
            if (isExist) throw new UserFriendlyException(L[EduZoneDomainErrorCodes.LicenseExist]);
            var newLicense = new License(GuidGenerator.Create(), input.Key, input.ExpirationDate, false);
            await _licenseRepository.InsertAsync(newLicense,true);
            return true;

        }

        public async Task<bool> UpdateLicanse(Guid id, CreateLicenseInput input)
        {
            var dbLicense = await _licenseRepository.FirstOrDefaultAsync(l => l.Id == id)
                ?? throw new UserFriendlyException(EduZoneDomainErrorCodes.NotFound);

            dbLicense.ExpirationDate = input.ExpirationDate;

            return true;

        }
    }
}
