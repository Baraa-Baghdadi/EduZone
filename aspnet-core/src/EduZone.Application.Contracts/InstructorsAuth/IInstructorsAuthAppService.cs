using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EduZone.InstructorsAuth
{
    public interface IInstructorsAuthAppService : IApplicationService
    {
        Task<bool> CreateNewInstructor(NewInstructorInput input);
        public Task<bool> Verify(VerifyCodeDto input);
    }
}
