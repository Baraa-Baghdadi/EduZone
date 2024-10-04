import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { LessonDto, LessonDtoForAddCourse } from '../lessons/models';

export interface CourseDto extends FullAuditedEntityDto<string> {
  title?: string;
  description?: string;
  price?: number;
  newPrice?: number;
  icon?: string;
  orginalImage?: string;
  fileType?: string;
  categoryId?: string;
  categoryName?: string;
  instructorName?: string;
  lessons: LessonDto[];
}

export interface GetCoursesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
}

export interface NewCourseInput {
  title: string;
  description: string;
  price: number;
  newPrice: number;
  blop: string;
  fileType: string;
  fileName: string;
  fileSize: number;
  isIconUpdated: boolean;
  categoryId: string;
  lessons: LessonDtoForAddCourse[];
}

export interface UpdateCourseInput {
  id: string;
  title: string;
  description: string;
  price: number;
  newPrice: number;
  blop?: string;
  fileType?: string;
  fileName?: string;
  fileSize?: number;
  isIconUpdated: boolean;
  categoryId: string;
  lessons: LessonDtoForAddCourse[];
}
