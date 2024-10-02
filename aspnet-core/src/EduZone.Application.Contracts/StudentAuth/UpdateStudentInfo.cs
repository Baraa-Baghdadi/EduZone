using EduZone.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.StudentAuth
{
    public class UpdateStudentInfo
    {
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public Gender? Gender { get; set; } = null;
        public DateTime? DOB { get; set; } = null;
    }
}
