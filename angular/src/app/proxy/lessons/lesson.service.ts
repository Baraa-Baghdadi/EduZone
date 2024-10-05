import type { LessonDto, UpdateLessonInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LessonService {
  apiName = 'Default';
  

  getLessonsByCourseIdById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LessonDto[]>({
      method: 'GET',
      url: `/api/app/lesson/${id}/lessons-by-course-id`,
    },
    { apiName: this.apiName,...config });
  

  updateLessonByInput = (input: UpdateLessonInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: '/api/app/lesson/lesson',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
