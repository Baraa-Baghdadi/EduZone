using EduZone.Lessons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Courses
{
    public class NewCourseInput
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double NewPrice { get; set; }
        [Required]
        public string Blop { get; set; }
        [Required]
        public string FileType { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public int FileSize { get; set; }
        [Required]
        public bool IsIconUpdated { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public List<LessonDtoForAddCourse> Lessons { get; set; } = [];
    }
}
