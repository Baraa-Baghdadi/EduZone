using EduZone.CreateCertificate;
using EduZone.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EduZone.Certificates
{
    public class CertificateAppService : EduZoneAppService, ICertificateAppService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly CreateCertificateService _createCertificateService;
        private readonly IDataFilter _dataFilter;

        public CertificateAppService(ICertificateRepository certificateRepository, IDataFilter dataFilter,
            CreateCertificateService createCertificateService)
        {
            _certificateRepository = certificateRepository;
            _createCertificateService = createCertificateService;
            _dataFilter = dataFilter;
        }

        public async Task<CertificateDto> GetCertificateById(Guid id)
        {
            var certificate = await _certificateRepository.GetCertificateById(id)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);
            var mappingData = ObjectMapper.Map<Certificate, CertificateDto>(certificate);
            return mappingData;
        }

        // For Admin:
        [Authorize(EduZonePermissions.AdminCertificates.GetCertificate)]
        public async Task<PagedResultDto<CertificateDto>> GetAllCertificates(GetCertificatesInput input)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var totalCount = await _certificateRepository.GetCountAsync(input.FilterText);
                var items = await _certificateRepository.GetListAsync(input.FilterText,
                    input.Sorting, input.MaxResultCount, input.SkipCount);
                return new PagedResultDto<CertificateDto>
                {
                    TotalCount = totalCount,
                    Items = ObjectMapper.Map<List<Certificate>, List<CertificateDto>>(items)
                };
            }
        }

        // For Instructor:
        [Authorize(EduZonePermissions.InstructorCertificates.GetCertificate)]
        public async Task<PagedResultDto<CertificateDto>> GetMyStudientsCertificates(GetCertificatesInput input)
        {
            var totalCount = await _certificateRepository.GetCountAsync(input.FilterText);
            var items = await _certificateRepository.GetListAsync(input.FilterText,
                input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<CertificateDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Certificate>, List<CertificateDto>>(items)
            };
        }

        // Download Certificate For Instructor:
        [Authorize(EduZonePermissions.InstructorCertificates.GetCertificate)]
        public async Task<IRemoteStreamContent> InstructorDownlaodCertificate(Guid id)
        {
            var certificate = await _certificateRepository.GetCertificateById(id) ??
                throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);

            var mappedCertificate = ObjectMapper.Map<Certificate, CertificateDto>(certificate);

            var certificatePath = await _createCertificateService.GenerateCertificate(mappedCertificate!.StudentName!, mappedCertificate!.CourseTitle!
                , mappedCertificate.CreationTime);

            if (File.Exists(certificatePath))
            {
                var certificateStream = new RemoteStreamContent(new MemoryStream(System.IO.File.ReadAllBytes(certificatePath)), $"{mappedCertificate!.CourseTitle! + " for " + mappedCertificate!.StudentName!}.pdf");
                File.Delete(certificatePath);
                return certificateStream;

            }
            return null;
        }

        // For Admin
        [Authorize(EduZonePermissions.AdminCertificates.GetCertificate)]
        public async Task<IRemoteStreamContent> AdminDownlaodCertificate(Guid id)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var certificate = await _certificateRepository.GetCertificateById(id) ??
                    throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);

                var mappedCertificate = ObjectMapper.Map<Certificate, CertificateDto>(certificate);

                var certificatePath = await _createCertificateService.GenerateCertificate(mappedCertificate!.StudentName!, mappedCertificate!.CourseTitle!
                    , mappedCertificate.CreationTime);

                if (File.Exists(certificatePath))
                {
                    var certificateStream = new RemoteStreamContent(new MemoryStream(System.IO.File.ReadAllBytes(certificatePath)), $"{mappedCertificate!.CourseTitle! + " for " + mappedCertificate!.StudentName!}.pdf");
                    File.Delete(certificatePath);
                    return certificateStream;

                }
                return null;
            }
        }

    }
}
