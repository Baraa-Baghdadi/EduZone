import type { CategoryDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  apiName = 'Default';
  

  getCategoriesForDrop = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, CategoryDto[]>({
      method: 'GET',
      url: '/api/app/category/categories-for-drop',
    },
    { apiName: this.apiName,...config });
  

  getGetgoryById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CategoryDto>({
      method: 'GET',
      url: `/api/app/category/${id}/getgory`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
