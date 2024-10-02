using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Lessons
{
    public class Lesson : FullAuditedAggregateRoot<Guid>
    {

        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Duration { get; set; }
        public decimal FileSize { get; set; }  // In MB
        public int VideoOrder { get; set; }
        public string Url { get; set; }

        public void SetId(Guid id) { Id = id; }

        public Lesson(Guid id,Guid courseId, string name, string title, string content, string duration, decimal fileSize, int videoOrder, 
            string url)
        {
            Id = id;
            CourseId = courseId;
            Name = name;
            Title = title;
            Content = content;
            Duration = duration;
            FileSize = fileSize;
            VideoOrder = videoOrder;
            Url = url;
        }

        public Lesson()
        {
            
        }
    }
}
