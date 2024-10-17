using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Certificates
{
    public class CertificateDto : FullAuditedEntityDto<Guid>
    {
        public string? StudentName { get; set; }
        public string? CourseTitle { get; set; }
        public string? InstructorName { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public decimal CreatedOn { get; set; }
    }
}
