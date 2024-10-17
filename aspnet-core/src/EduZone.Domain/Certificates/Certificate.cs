using EduZone.Courses;
using EduZone.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Certificates
{
    public class Certificate : FullAuditedAggregateRoot<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public decimal CreatedOn { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }


        public Certificate(Guid id,Guid studentId, Guid courseId, decimal createdOn)
        {
            Id = id;
            StudentId = studentId;
            CourseId = courseId;
            CreatedOn = createdOn;
        }

        private Certificate()
        {
            
        }


    }
}
