import type { NewInstructorInput, VerifyCodeDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class InstructorAuthService {
  apiName = 'Default';
  

  createNewInstructorByInput = (input: NewInstructorInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/instructor-auth/new-instructor',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  verifyByInput = (input: VerifyCodeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/instructor-auth/verify',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
