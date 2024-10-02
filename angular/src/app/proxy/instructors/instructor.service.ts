import type { GetInstructorInput, InstructorDto } from './models';
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

  constructor(private restService: RestService) {}
}
