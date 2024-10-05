using EduZone.Courses;
using EduZone.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Ratings
{
    public class Rate : FullAuditedAggregateRoot<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public double Value { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

        public Rate(Guid id,Guid studentId, Guid courseId, double value)
        {
            Id = id;
            StudentId = studentId;
            CourseId = courseId;
            Value = value;
        }
        protected Rate()
        {

        }
    }
}
