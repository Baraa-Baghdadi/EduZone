using EduZone.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EduZone.Hub
{
    public interface IHubClient : IApplicationService
    {
        Task StudentAddedYouMsg(NewStudentMsg msg);
    }
}
