using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EduZone.Notifications
{
    public interface INotificationInstructorAppService : IApplicationService
    {
        Task CreateNewEnrollmentNotification(Guid studentId, Guid instructorId, Guid enrollId,string courseName, string content, NotificationTypeEnum type, Dictionary<string, string> extraproperties);
        Task<PagedResultDto<InstructorNotificationDto>> GetListOfInstructorNotification(PagedAndSortedResultRequestDto input);
        Task MarkAllAsRead();
        Task<int> GetCountOfUnreadingMsgAsync();
    }
}
