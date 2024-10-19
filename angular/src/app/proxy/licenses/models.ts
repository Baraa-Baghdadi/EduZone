import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateLicenseInput {
  key: string;
  expirationDate: string;
}

export interface GetLicenseInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
}

export interface LicenseDto extends EntityDto<string> {
  key?: string;
  expirationDate?: string;
  isUsed: boolean;
}
