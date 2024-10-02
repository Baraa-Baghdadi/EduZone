using EduZone.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Instructors
{
    public class GetInstructorInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public Gender? Gender { get; set; }
    }
}
