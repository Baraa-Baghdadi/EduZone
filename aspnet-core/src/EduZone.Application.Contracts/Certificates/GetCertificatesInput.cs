using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Certificates
{
    public class GetCertificatesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
    }
}
