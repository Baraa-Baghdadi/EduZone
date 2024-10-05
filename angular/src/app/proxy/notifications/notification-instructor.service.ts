import type { InstructorNotificationDto } from './models';
import type { NotificationTypeEnum } from './notification-type-enum.enum';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NotificationInstructorService {
  apiName = 'Default';
  

  createNewEnrollmentNotificationByStudentIdAndInstructorIdAndEnrollIdAndCourseNameAndContentAndTypeAndExtraproperties = (studentId: string, instructorId: string, enrollId: string, courseName: string, content: string, type: NotificationTypeEnum, extraproperties: Record<string, string>, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/notification-instructor/new-enrollment-notification',
      params: { studentId, instructorId, enrollId, courseName, content, type },
      body: extraproperties,
    },
    { apiName: this.apiName,...config });
  

  getCountOfUnreadingMsg = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, number>({
      method: 'GET',
      url: '/api/app/notification-instructor/count-of-unreading-msg',
    },
    { apiName: this.apiName,...config });
  

  getListOfInstructorNotificationByInput = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<InstructorNotificationDto>>({
      method: 'GET',
      url: '/api/app/notification-instructor/of-instructor-notification',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  markAllAsRead = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/notification-instructor/mark-all-as-read',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
