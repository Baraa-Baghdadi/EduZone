using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.GenerateCertificate
{
    public class CertificateInfo
    {
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
