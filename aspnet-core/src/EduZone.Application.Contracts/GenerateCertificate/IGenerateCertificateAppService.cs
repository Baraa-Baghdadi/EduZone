using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.GenerateCertificate
{
    public interface IGenerateCertificateAppService
    {
        Task<bool> GenerateService(CertificateInfo input);
    }
}
