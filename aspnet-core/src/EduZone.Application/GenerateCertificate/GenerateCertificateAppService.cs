using EduZone.CreateCertificate;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.GenerateCertificate
{
    [Authorize]
    public class GenerateCertificateAppService : EduZoneAppService, IGenerateCertificateAppService
    {
        private readonly CreateCertificateService _createCertificateService;

        public GenerateCertificateAppService(CreateCertificateService createCertificateService)
        {
            _createCertificateService = createCertificateService;
        }

        public async Task<bool> GenerateService(CertificateInfo input)
        {
            await _createCertificateService.GenerateCertificate(input.StudentName, input.CourseName, input.Date);
            return true;
        }
    }
}
