import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface EnrollmentDto extends FullAuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  courseTitle?: string;
  categoryName?: string;
  courseIcon?: string;
  enrollmentDate?: number;
}

export interface GetEnrollmentInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
}

export interface NewEnrollmentInput {
  courseId: string;
}
