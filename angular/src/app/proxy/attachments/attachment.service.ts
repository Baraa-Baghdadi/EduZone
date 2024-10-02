import type { AttachmentCreateDto, AttachmentDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AttachmentService {
  apiName = 'Default';
  

  createAttachment = (input: AttachmentCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AttachmentDto>({
      method: 'POST',
      url: '/api/app/Attachment/CreateAttachmentAsync',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  deleteImageByImageId = (imageId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/Attachment/DeleteImage',
      params: { imageId },
    },
    { apiName: this.apiName,...config });
  

  getImageByImageId = (imageId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/Attachment/GetImage',
      params: { imageId },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
