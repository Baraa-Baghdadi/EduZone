using EduZone.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace EduZone.Notifications
{
    public class EfCoreNotificationRepository : EfCoreRepository<EduZoneDbContext, Notification, Guid>, INotificationRepository
    {
        public EfCoreNotificationRepository(IDbContextProvider<EduZoneDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<(IEnumerable<Notification>, int)> GetInstructorNotifications(Guid tenantId, int skipCount, int maxResultCount, string? sorting)
        {
            var notification = (await GetQueryableAsync())
            .WhereIf(true, n => n.TenantId == tenantId)
            .OrderBy(string.IsNullOrEmpty(sorting) ? NotificationConst.GetDefaultSorting(false) : sorting)
            .AsEnumerable();

            var count = notification.Count();
            notification = notification.Skip(skipCount).Take(maxResultCount);
            return (notification, count);
        }
    }
}
