import type { EntityDto } from '@abp/ng.core';

export interface CategoryDto extends EntityDto<string> {
  name?: string;
  description?: string;
}
