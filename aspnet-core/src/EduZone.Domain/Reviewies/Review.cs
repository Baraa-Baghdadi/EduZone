using EduZone.Courses;
using EduZone.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Reviewies
{
    public class Review : FullAuditedAggregateRoot<Guid>
    {

        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public decimal CreationOn { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }

        public Review(Guid id, Guid courseId, Guid studentId, double rating, string comment, decimal creationOn)
        {
            Id = id;
            CourseId = courseId;
            StudentId = studentId;
            Rating = rating;
            Comment = comment;
            CreationOn = creationOn;
        }
    }
}
