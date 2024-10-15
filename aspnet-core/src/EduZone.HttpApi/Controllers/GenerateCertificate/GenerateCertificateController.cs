using Asp.Versioning;
using EduZone.GenerateCertificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EduZone.Controllers.GenerateCertificate
{
    [Authorize]
    [RemoteService]
    [Area("app")]
    [ControllerName("Certificate")]
    [Route("api/app/Certificate")]
    public class GenerateCertificateController : AbpController, IGenerateCertificateAppService
    {
        private readonly IGenerateCertificateAppService _appService;

        public GenerateCertificateController(IGenerateCertificateAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("GenerateCertificate")]
        public async Task<bool> GenerateCertificate(CertificateInfo input)
        {
            return await _appService.GenerateCertificate(input);
        }
    }
}
