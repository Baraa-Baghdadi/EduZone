import type { CountryCodeDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CountryCodeService {
  apiName = 'Default';
  

  getCountryCodes = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryCodeDto[]>({
      method: 'GET',
      url: '/api/app/country-code/country-codes',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
