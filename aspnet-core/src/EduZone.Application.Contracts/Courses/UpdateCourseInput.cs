using EduZone.Lessons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Courses
{
    public class UpdateCourseInput
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double NewPrice { get; set; }
        public string? Blop { get; set; }
        
        public string? FileType { get; set; }
        
        public string? FileName { get; set; }
      
        public int? FileSize { get; set; }

        [Required]
        public bool IsIconUpdated { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public List<LessonDtoForAddCourse> Lessons { get; set; } = [];
    }
}
