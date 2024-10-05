using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Notifications
{
    public interface INotificationRepository : IRepository<Notification, Guid>
    {
        Task<(IEnumerable<Notification>, int)> GetInstructorNotifications(Guid tenantId, int skipCount, 
            int maxResultCount, string? sorting);
    }
}
