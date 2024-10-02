import type { NewStudentInput, UpdateStudentInfo } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { Response } from '../api-response/models';

@Injectable({
  providedIn: 'root',
})
export class StudentAuthService {
  apiName = 'Default';
  

  addNewStudentByInput = (input: NewStudentInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Response<boolean>>({
      method: 'POST',
      url: '/api/app/StudentAuth/CreateNewStudent',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateStudentInfoByInput = (input: UpdateStudentInfo, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Response<boolean>>({
      method: 'PUT',
      url: '/api/app/StudentAuth/UpdateStudentInfo',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
