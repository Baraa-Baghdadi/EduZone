import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface LessonDto extends FullAuditedEntityDto<string> {
  name?: string;
  title?: string;
  content?: string;
  duration?: string;
  fileSize: number;
  videoOrder: number;
  url?: string;
}

export interface LessonDtoForAddCourse {
  id?: string;
  name?: string;
  title?: string;
  content?: string;
  duration?: string;
  fileSize: number;
  videoOrder: number;
  url?: string;
}

export interface UpdateLessonInput {
  id?: string;
  title?: string;
  content?: string;
  videoOrder: number;
}
