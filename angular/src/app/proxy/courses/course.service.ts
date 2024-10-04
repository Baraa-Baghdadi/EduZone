import type { CourseDto, GetCoursesInput, NewCourseInput, UpdateCourseInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  apiName = 'Default';
  

  createNewCourseByInput = (input: NewCourseInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CourseDto>({
      method: 'POST',
      url: '/api/app/course/new-course',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getAllCoursesByInput = (input: GetCoursesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CourseDto>>({
      method: 'GET',
      url: '/api/app/course/courses',
      params: { filterText: input.filterText, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getCourseByIdById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CourseDto>({
      method: 'GET',
      url: `/api/app/course/${id}/course-by-id`,
    },
    { apiName: this.apiName,...config });
  

  getMyCoursesByInput = (input: GetCoursesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CourseDto>>({
      method: 'GET',
      url: '/api/app/course/my-courses',
      params: { filterText: input.filterText, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  updateCourse = (input: UpdateCourseInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CourseDto>({
      method: 'PUT',
      url: '/api/app/course/course',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
