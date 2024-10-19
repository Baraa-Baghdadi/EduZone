import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetLicenseInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
}

export interface LicenseDto {
  key?: string;
  expirationDate?: string;
  isUsed: boolean;
}
