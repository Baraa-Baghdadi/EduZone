import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CertificateDto extends FullAuditedEntityDto<string> {
  studentName?: string;
  courseTitle?: string;
  instructorName?: string;
  studentId?: string;
  courseId?: string;
  createdOn: number;
}

export interface GetCertificatesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
}
