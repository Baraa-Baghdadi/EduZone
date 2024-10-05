import { mapEnumToOptions } from '@abp/ng.core';

export enum NotificationTypeEnum {
  NewEnrollment = 0,
}

export const notificationTypeEnumOptions = mapEnumToOptions(NotificationTypeEnum);
