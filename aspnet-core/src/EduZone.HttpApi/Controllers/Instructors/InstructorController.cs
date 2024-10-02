using Asp.Versioning;
using EduZone.Instructors;
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

namespace EduZone.Controllers.Instructors
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Admin Teacher Info")]
    [Route("api/app/Teacher")]
    public class InstructorController : AbpController, IInstructorsAppService
    {
        private readonly IInstructorsAppService _instructorsAppService;

        public InstructorController(IInstructorsAppService instructorsAppService)
        {
            _instructorsAppService = instructorsAppService;
        }

        [HttpGet("GetAllInstructor")]
        public async Task<PagedResultDto<InstructorDto>> GetAllInstructor(GetInstructorInput input)
        {
            return await _instructorsAppService.GetAllInstructor(input);
        }

        [HttpGet("GetInstructorById")]
        public async Task<InstructorDto> GetInstructorById([Required] Guid id)
        {
            return await _instructorsAppService.GetInstructorById(id);
        }
    }
}
