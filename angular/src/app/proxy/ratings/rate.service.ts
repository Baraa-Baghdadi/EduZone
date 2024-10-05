import type { GetCourseRatingOfInstructor, RateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RateService {
  apiName = 'Default';
  

  getRateMyCourseByInput = (input: GetCourseRatingOfInstructor, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<RateDto>>({
      method: 'GET',
      url: '/api/app/rate/rate-my-course',
      params: { filterText: input.filterText, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  rateCourseByCourseIdAndRating = (courseId: string, rating: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: `/api/app/rate/rate-course/${courseId}`,
      params: { rating },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
