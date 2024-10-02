using EduZone.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.InstructorsAuth
{
    public class NewInstructorInput
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [AllowNull]
        public string About { get; set; }
    }
}
