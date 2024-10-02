using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EduZone.Lessons
{
    public class LessonDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Duration { get; set; }
        public decimal FileSize { get; set; }  // In MB
        public int VideoOrder { get; set; }
        public string Url { get; set; }
    }
}
