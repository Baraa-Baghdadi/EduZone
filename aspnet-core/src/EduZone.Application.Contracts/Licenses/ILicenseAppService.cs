using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Licenses
{
    public interface ILicenseAppService
    {
        Task<PagedResultDto<LicenseDto>> GetListAsync(GetLicenseInput input); // get all for admin
        Task<LicenseDto> GetById(Guid id); // get single for admin

    }
}
