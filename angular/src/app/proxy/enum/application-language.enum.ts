import { mapEnumToOptions } from '@abp/ng.core';

export enum ApplicationLanguage {
  en = 0,
  ar = 1,
}

export const applicationLanguageOptions = mapEnumToOptions(ApplicationLanguage);
