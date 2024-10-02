using EduZone.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Students
{
    public class StudentDto : FullAuditedEntityDto<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public ApplicationLanguage? CurrentLanguage { get; set; }
    }
}
