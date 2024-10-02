using EduZone.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EduZone.StudentAuth
{
    public interface IStudentAuthAppService : IApplicationService
    {
       Task<Response<bool>> AddNewStudent(NewStudentInput input);
       Task<Response<bool>> UpdateStudentInfo(UpdateStudentInfo input);
    }
}
