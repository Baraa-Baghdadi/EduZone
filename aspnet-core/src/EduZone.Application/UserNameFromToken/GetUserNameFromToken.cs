using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Users;

namespace EduZone.UserNameFromToken
{
    [RemoteService(IsEnabled = false)]
    public class GetUserNameFromToken : EduZoneAppService , IGetUserNameFromToken
    {
        private readonly ICurrentUser _currentUser;

        public GetUserNameFromToken(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }


        public string GetEmailFromToken()
        {
            return _currentUser.Email ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.UserNotFound]);

        }
    }
}
