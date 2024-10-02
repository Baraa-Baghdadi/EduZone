using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Enrollments
{
    public class EnrollmentDto : FullAuditedEntityDto<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CourseTitle { get; set; }
        public string? CategoryName { get; set; }
        public string? CourseIcon { get; set; } // as thumbnail
        public decimal? EnrollmentDate { get; set; }

    }
}
