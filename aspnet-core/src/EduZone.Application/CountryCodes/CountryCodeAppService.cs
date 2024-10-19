using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.CountryCodes
{
    public class CountryCodeAppService : EduZoneAppService, ICountryCodeAppService
    {
        private readonly IRepository<CountryCode> _countryCodeRepository;

        public CountryCodeAppService(IRepository<CountryCode> countryCodeRepository)
        {
            _countryCodeRepository = countryCodeRepository;
        }

        public async Task<List<CountryCodeDto>> GetCountryCodesAsync()
        {
            var countryCodes = await _countryCodeRepository.GetListAsync();
            return ObjectMapper.Map<List<CountryCode>, List<CountryCodeDto>>(countryCodes);
        }
    }
}
