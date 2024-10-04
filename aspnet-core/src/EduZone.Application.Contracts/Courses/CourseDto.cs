using EduZone.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Courses
{
    public class CourseDto : FullAuditedEntityDto<Guid>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public double? NewPrice { get; set; }
        public string? Icon { get; set; } // as thumbnail
        public string? OrginalImage { get; set; } // return Orginal image for showing it as base 64
        public string? FileType { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? InstructorName { get; set; }
        public List<LessonDto>? Lessons { get; set; }

    }
}
