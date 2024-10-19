using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Licenses
{
    public class LicenseDto : EntityDto<Guid>
    {
        public string Key { get; set; } // length is 6
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
