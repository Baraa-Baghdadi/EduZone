using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Licenses
{
    public class License : FullAuditedAggregateRoot<Guid>
    {
        [Length(6,6)]
        public string Key { get; set; } // length is 6
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }


        public License(Guid id,string key, DateTime expirationDate, bool isUsed)
        {
            Id = id;
            Key = key;
            ExpirationDate = expirationDate;
            IsUsed = isUsed;
        }
    }
}
