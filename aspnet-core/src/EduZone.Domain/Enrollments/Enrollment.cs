using EduZone.Courses;
using EduZone.Enum;
using EduZone.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Enrollments
{
    public class Enrollment : FullAuditedEntity<Guid>
    {

        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public bool IsInWishList { get; set; } = false;
        public decimal EnrollmentDate { get; set; }
        public decimal? CompletionDate { get; set; } = null;
        public CourseStatus CourseStatus { get; set; } = CourseStatus.Enrolled;
        public Student Student { get; set; }
        public Course Course { get; set; }


        protected Enrollment()
        {
            
        }

        public Enrollment(Guid id,Guid studentId, Guid courseId, bool isInWishList, decimal enrollmentDate, decimal? completionDate,
            CourseStatus courseStatus)
        {
            Id = id;
            StudentId = studentId;
            CourseId = courseId;
            IsInWishList = isInWishList;
            EnrollmentDate = enrollmentDate;
            CompletionDate = completionDate;
            CourseStatus = courseStatus;
        }
    }
}
