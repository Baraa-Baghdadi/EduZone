import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { Gender } from '../enum/gender.enum';
import type { ApplicationLanguage } from '../enum/application-language.enum';

export interface GetStudentInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  gender?: Gender;
}

export interface StudentDto extends FullAuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  gender?: Gender;
  email?: string;
  dob?: string;
  currentLanguage?: ApplicationLanguage;
}
