using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Emailing
{
    public class EmailSendingArgs
    {
        public string? Template { get; set; }
        public string? TargetEmail { get; set; }
        public string? ConfirmationLink { get; set; }
        public string? EmailPlaceHolder { get; set; }
        public string? InstructorNamePlaceHolder { get; set; }
        public string? TenantPlaceHolder { get; set; }
        public Guid InstructerId { get; set; }
    }
}
