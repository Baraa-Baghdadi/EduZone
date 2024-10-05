import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { Gender } from '../enum/gender.enum';

export interface GetInstructorInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  gender?: Gender;
}

export interface InstructorDto extends FullAuditedEntityDto<string> {
  tenantId?: string;
  firstName?: string;
  lastName?: string;
  gender?: Gender;
  email?: string;
  about?: string;
  fullMobileNumber?: string;
}

export interface UpdateInstructorInfoInput {
  firstName: string;
  lastName: string;
  gender: Gender;
  about: string;
}
