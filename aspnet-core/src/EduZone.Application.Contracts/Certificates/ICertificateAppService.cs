using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace EduZone.Certificates
{
    public interface ICertificateAppService 
    {
        Task<PagedResultDto<CertificateDto>> GetMyStudientsCertificates(GetCertificatesInput input); // For instructor
        Task<PagedResultDto<CertificateDto>> GetAllCertificates(GetCertificatesInput input); // For admin
        Task<CertificateDto> GetCertificateById(Guid id);
        Task<IRemoteStreamContent> InstructorDownlaodCertificate(Guid id);
        Task<IRemoteStreamContent> AdminDownlaodCertificate(Guid id);


    }
}
