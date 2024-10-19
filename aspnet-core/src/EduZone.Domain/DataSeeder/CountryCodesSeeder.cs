using EduZone.CountryCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace EduZone.DataSeeder
{
    public class CountryCodesSeeder : ITransientDependency
    {
        private readonly IRepository<CountryCode> _countryCodeRepo;
        private readonly IGuidGenerator _guidGenerator;

        public CountryCodesSeeder(IRepository<CountryCode> countryCodeRepo, IGuidGenerator guidGenerator)
        {
            _countryCodeRepo = countryCodeRepo;
            _guidGenerator = guidGenerator;
        }

        public async Task Seed()
        {
            await CreateCountryCodes();
        }
        private async Task CreateCountryCodes()
        {
            if (!await _countryCodeRepo.AnyAsync())
            {
                List<CountryCode> countryCodes = new List<CountryCode>
                {
                    new CountryCode("+971 ","United Arab Emirates","https://flagsapi.com/AE/flat/24.png", "5XX XXX XXX"),
                    new CountryCode("+963","Syrian Arab Republic","https://flagsapi.com/SY/flat/24.png", "9XX XXX XXX"),
                    new CountryCode("+961","Lebanon","https://flagsapi.com/LB/flat/24.png", "7X XXX XXX"),
                    new CountryCode("+965","Kuwait","https://flagsapi.com/KW/flat/24.png", "5XX XXX XXX"),
                    new CountryCode("+974","Qatar","https://flagsapi.com/QA/flat/24.png", "3XX XXXX"),
                    new CountryCode("+1","United States","https://flagsapi.com/US/flat/24.png", "XXX-XXX-XXXX"),
                    new CountryCode("+962","Jordan","https://flagsapi.com/JO/flat/24.png", "7X XXX XXXX"),
                };

                await _countryCodeRepo.InsertManyAsync(countryCodes,true);

            }
        }
    }
}
