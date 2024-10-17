import type { CertificateDto, GetCertificatesInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CertificateService {
  apiName = 'Default';
  

  adminDownlaodCertificateById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'POST',
      responseType: 'blob',
      url: `/api/app/certificate/${id}/admin-downlaod-certificate`,
    },
    { apiName: this.apiName,...config });
  

  getAllCertificatesByInput = (input: GetCertificatesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CertificateDto>>({
      method: 'GET',
      url: '/api/app/certificate/certificates',
      params: { filterText: input.filterText, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getCertificateByIdById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CertificateDto>({
      method: 'GET',
      url: `/api/app/certificate/${id}/certificate-by-id`,
    },
    { apiName: this.apiName,...config });
  

  getMyStudientsCertificatesByInput = (input: GetCertificatesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CertificateDto>>({
      method: 'GET',
      url: '/api/app/certificate/my-studients-certificates',
      params: { filterText: input.filterText, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  instructorDownlaodCertificateById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'POST',
      responseType: 'blob',
      url: `/api/app/certificate/${id}/instructor-downlaod-certificate`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
