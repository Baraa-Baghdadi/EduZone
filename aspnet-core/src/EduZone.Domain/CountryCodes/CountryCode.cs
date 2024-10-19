using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace EduZone.CountryCodes
{
    public class CountryCode : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public string ExampleNumber { get; set; }

        public CountryCode(string code, string name, string flag, string exampleNumber)
        {
            Code = code;
            Name = name;
            Flag = flag;
            ExampleNumber = exampleNumber;
        }
    }
}
