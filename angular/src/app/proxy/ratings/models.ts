import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetCourseRatingOfInstructor extends PagedAndSortedResultRequestDto {
  filterText?: string;
}

export interface RateDto {
  courseId?: string;
  studentName?: string;
  courseName?: string;
  category?: string;
  rate?: number;
}
