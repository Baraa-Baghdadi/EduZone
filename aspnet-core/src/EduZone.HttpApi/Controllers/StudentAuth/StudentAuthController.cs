using Asp.Versioning;
using EduZone.ApiResponse;
using EduZone.StudentAuth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EduZone.Controllers.StudentAuth
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Student Auth")]
    [Route("api/app/StudentAuth")]
    public class StudentAuthController : AbpController, IStudentAuthAppService
    {
        private readonly IStudentAuthAppService _studentAuthAppService;

        public StudentAuthController(IStudentAuthAppService studentAuthAppService)
        {
            _studentAuthAppService = studentAuthAppService;
        }

        [HttpPost("CreateNewStudent")]
        public async Task<Response<bool>> AddNewStudent(NewStudentInput input)
        {
            return await _studentAuthAppService.AddNewStudent(input);
        }

        [HttpPut("UpdateStudentInfo")]
        public async Task<Response<bool>> UpdateStudentInfo(UpdateStudentInfo input)
        {
            return await _studentAuthAppService.UpdateStudentInfo(input);
        }
    }
}
