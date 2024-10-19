using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Licenses
{
    public class LicenseDto
    {
        public string Key { get; set; } // length is 6
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
