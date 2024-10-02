using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Courses
{
    public class GetCoursesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
    }
}
