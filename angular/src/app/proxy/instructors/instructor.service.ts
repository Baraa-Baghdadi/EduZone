import type { GetInstructorInput, InstructorDto, UpdateInstructorInfoInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class InstructorService {
  apiName = 'Default';
  

  getAllInstructorByInput = (input: GetInstructorInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<InstructorDto>>({
      method: 'GET',
      url: '/api/app/Teacher/GetAllInstructor',
      params: { filterText: input.filterText, gender: input.gender, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getInstructorByIdById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InstructorDto>({
      method: 'GET',
      url: '/api/app/Teacher/GetInstructorById',
      params: { id },
    },
    { apiName: this.apiName,...config });
  

  getInstructorInfo = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, InstructorDto>({
      method: 'GET',
      url: '/api/app/Teacher/GetInstructorInfo',
    },
    { apiName: this.apiName,...config });
  

  updateInstructorInfoByInput = (input: UpdateInstructorInfoInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: '/api/app/Teacher/UpdateInstructorInfo',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
