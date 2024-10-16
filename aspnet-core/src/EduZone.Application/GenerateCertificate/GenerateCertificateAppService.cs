using EduZone.CreateCertificate;
using EduZone.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EduZone.GenerateCertificate
{
    [RemoteService(IsEnabled = false)]
    public class GenerateCertificateAppService : EduZoneAppService, IGenerateCertificateAppService
    {
        private readonly CreateCertificateService _createCertificateService;

        public GenerateCertificateAppService(CreateCertificateService createCertificateService)
        {
            _createCertificateService = createCertificateService;
        }

        //[Authorize(EduZonePermissions.AdminCertificates.GenerateCertificate)]
        public async Task<bool> GenerateCertificate(CertificateInfo input)
        {
            await _createCertificateService.GenerateCertificate(input.StudentName, input.CourseName, input.Date);
            return true;
        }
    }
}
