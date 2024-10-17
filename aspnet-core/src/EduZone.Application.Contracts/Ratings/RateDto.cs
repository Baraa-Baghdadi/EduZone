using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Ratings
{
    public class RateDto
    {
        public Guid CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? Category { get; set; }
        public double? Rate { get; set; }
    }
}
