using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Licenses
{
    public class CreateLicenseInput
    {
        [Required]
        public string Key { get; set; } // length is 6
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
