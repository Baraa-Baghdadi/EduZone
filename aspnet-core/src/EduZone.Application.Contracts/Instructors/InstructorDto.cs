using EduZone.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Instructors
{
    public class InstructorDto : FullAuditedEntityDto<Guid>
    {
        public Guid TenantId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public string? Email { get; set; }
        public string? About { get; set; }
        public string? CountryCode { get; set; }
        public string? MobileNumber { get; set; }
        public string? FullMobileNumber { get; set; }

    }
}
