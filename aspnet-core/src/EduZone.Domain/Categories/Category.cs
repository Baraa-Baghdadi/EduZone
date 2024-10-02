using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Categories
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public Category(Guid id,string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
