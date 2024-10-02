import type { EnrollmentDto, GetEnrollmentInput, NewEnrollmentInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EnrollmentService {
  apiName = 'Default';
  

  addNewEnrollByInput = (input: NewEnrollmentInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EnrollmentDto>({
      method: 'POST',
      url: '/api/app/enrollment/new-enroll',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getEnrollmentByIdById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EnrollmentDto>({
      method: 'GET',
      url: `/api/app/enrollment/${id}/enrollment-by-id`,
    },
    { apiName: this.apiName,...config });
  

  getEnrollmentsOfInstructorByInput = (input: GetEnrollmentInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<EnrollmentDto>>({
      method: 'GET',
      url: '/api/app/enrollment/enrollments-of-instructor',
      params: { filterText: input.filterText, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
