using EduZone.Enrollments;
using EduZone.Hub;
using EduZone.Instructors;
using EduZone.Students;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace EduZone.Notifications
{
    public class NotificationInstructorAppService : EduZoneAppService, INotificationInstructorAppService
    {
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<Student> _studentRepo;
        private readonly INotificationRepository _notificationRepo;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        private readonly BroadcastHub _hub;
        private readonly IDataFilter _dataFilter;

        public NotificationInstructorAppService(IEnrollmentRepository enrollmentRepo,
            IRepository<Instructor> instructorRepository, IRepository<Student> studentRepo, 
            INotificationRepository notificationRepo, IHubContext<BroadcastHub, IHubClient> hubContext, 
            BroadcastHub hub, IDataFilter dataFilter)
        {
            _enrollmentRepo = enrollmentRepo;
            _instructorRepository = instructorRepository;
            _studentRepo = studentRepo;
            _notificationRepo = notificationRepo;
            _hubContext = hubContext;
            _hub = hub;
            _dataFilter = dataFilter;
        }

        public async Task CreateNewEnrollmentNotification(Guid id, string content, NotificationTypeEnum type, Dictionary<string, string> extraproperties)
        {
            // Get enrollment:
            var enrollment = await _enrollmentRepo.GetByIdWithDetails(id)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);

            var student = await _studentRepo.FirstOrDefaultAsync(row => row.Id == enrollment.StudentId);

            using (_dataFilter.Disable<IMultiTenant>())
            {
                // Get Provider:
                var instructor = await _instructorRepository.FirstOrDefaultAsync(row => row.Id == enrollment.Course.InstructorId)
                    ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);

                // title for notification:
                var title = instructor.FirstName + " " +instructor.LastName;

                var notification = new Notification
                {
                    EntityId = id,
                    Title = title,
                    Content = content,
                    IconThumbnail = "",
                    IsRead = false,
                    Type = type,
                    TenantId = instructor.TenantId,
                    CreatedOn = ServiceHelper.getTimeSpam(DateTime.UtcNow)!.Value
                };

                // save json in extraProperty Column in DB:
                if (!extraproperties.IsNullOrEmpty())
                {
                    foreach (var item in extraproperties.Keys)
                    {
                        notification.SetProperty(item, extraproperties[item]);
                    }
                }

                await _notificationRepo.InsertAsync(notification, true);

                var allConnections = _hub.getAllConnectionsId();

                if (allConnections.Any())
                {
                    var tenantId = instructor.TenantId ?? Guid.Empty;
                    if (allConnections.ContainsKey(tenantId))
                    {
                        var connectionsId = allConnections[tenantId];

                        if (connectionsId is not null)
                        {
                            foreach (var connectionId in connectionsId)
                            {
                                await sendMessage(connectionId, student!.FirstName + " " + student.LastName);
                            }
                        }
                    }

                }
            }
        }

        public async Task<PagedResultDto<InstructorNotificationDto>> GetListOfInstructorNotification(PagedAndSortedResultRequestDto input)
        {
            var currentTenant = CurrentUser.TenantId
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);

            (var notifications, int count) = await _notificationRepo.GetInstructorNotifications(currentTenant,
                input.SkipCount, input.MaxResultCount, input.Sorting);

            var mappingData = ObjectMapper.Map<IEnumerable<Notification>,
                IEnumerable<InstructorNotificationDto>>(notifications).ToList();

            var requiredData = new PagedResultDto<InstructorNotificationDto>
            {
                TotalCount = count,
                Items = notifications.Select(x => new InstructorNotificationDto
                {
                    Id = x.Id,
                    IsRead = x.IsRead,
                    EntityId = x.EntityId,
                    Title = L[x.Title],
                    CreatedOn = GetRelativeDate(UnixTimeStampToDateTime((double)x.CreatedOn)),
                    CreationTime = x.CreationTime,
                    Type = x.Type,
                    Content = L[x.Content, x.GetProperty("studentName") ?? ""]
                }).ToList()
            };

            return requiredData;
        }

        public async Task<int> GetCountOfUnreadingMsgAsync()
        {
            var currentTenant = CurrentUser.TenantId
            ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);
            var count = await _notificationRepo.CountAsync(row => row.TenantId == currentTenant && !row.IsRead);
            return count;
        }

        public async Task MarkAllAsRead()
        {
            var currentTenant = CurrentUser.TenantId
            ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);
            var notifications = await _notificationRepo.GetListAsync(row => row.TenantId == currentTenant && !row.IsRead);
            notifications.ForEach(notification => notification.IsRead = true);
            await _notificationRepo.UpdateManyAsync(notifications);
        }


        #region methods
        private async Task sendMessage(string connectionId, string msg)
        {
            await _hubContext.Clients.Client(connectionId).StudentAddedYouMsg(msg);
        }

        private string GetRelativeDate(DateTime date)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            switch (delta)
            {
                case < 60:
                    return L["justNow"];
                case < 120:
                    return L["oneMinuteAgo"];
                case < 3600:
                    return L["minutesAgo", ts.Minutes];
                case < 7200:
                    return L["oneHourAgo"];
                case < 86400:
                    return L["hoursAgo", ts.Hours];
                default:
                    if (date.Date == DateTime.Now.Date)
                    {
                        return L["today"];
                    }
                    else if (date.Date == DateTime.Now.AddDays(-1).Date)
                    {
                        return L["yesterday"];
                    }
                    else
                    {
                        return date.ToString("yyyy/MM/dd");
                    }
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        #endregion


    }
}
