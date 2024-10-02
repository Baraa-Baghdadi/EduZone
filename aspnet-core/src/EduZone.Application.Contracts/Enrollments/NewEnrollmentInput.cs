using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Enrollments
{
    public class NewEnrollmentInput
    {
        [Required]
        public Guid CourseId { get; set; }
    }
}
