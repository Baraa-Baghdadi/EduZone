import type { GetLicenseInput, LicenseDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LicenseService {
  apiName = 'Default';
  

  getByIdById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LicenseDto>({
      method: 'GET',
      url: `/api/app/license/${id}/by-id`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetLicenseInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LicenseDto>>({
      method: 'GET',
      url: '/api/app/license',
      params: { filterText: input.filterText, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
