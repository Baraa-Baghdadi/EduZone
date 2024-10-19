using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.CountryCodes
{
    public interface ICountryCodeAppService   
    {
        Task<List<CountryCodeDto>> GetCountryCodesAsync();
    }
}
