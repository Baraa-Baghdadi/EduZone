using Asp.Versioning;
using EduZone.Attachments;
using EduZone.Students;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace EduZone.Controllers.Students
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Admin Student Info")]
    [Route("api/app/Student")]
    public class StudentController : AbpController, IStudentAppService
    {
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        [HttpGet("GetAllAsync")]
        public async Task<PagedResultDto<StudentDto>> GetAllStudent(GetStudentInput input)
        {
            return await _studentAppService.GetAllStudent(input);
        }

        [HttpGet("GetAsync")]
        public async Task<StudentDto> GetStudentById([Required] Guid id)
        {
            return await _studentAppService.GetStudentById(id);
        }
    }
}
