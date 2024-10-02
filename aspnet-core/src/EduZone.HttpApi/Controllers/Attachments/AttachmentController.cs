using Asp.Versioning;
using EduZone.Attachments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EduZone.Controllers.Attachments
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Attachment API")]
    [Route("api/app/Attachment")]
    public class AttachmentController : AbpController, IAttachmentAppService
    {
        private readonly IAttachmentAppService _attachmentAppService;

        public AttachmentController(IAttachmentAppService attachmentAppService)
        {
            _attachmentAppService = attachmentAppService;
        }

        [HttpPost("CreateAttachmentAsync")]

        public async Task<AttachmentDto> CreateAttachmentAsync(AttachmentCreateDto input)
        {
            return await _attachmentAppService.CreateAttachmentAsync(input);
        }

        [HttpDelete("DeleteImage")]
        public async Task DeleteImage(Guid imageId)
        {
            await _attachmentAppService.DeleteImage(imageId);
        }

        [HttpGet("GetImage")]
        public async Task<byte[]> GetImage(Guid? imageId)
        {
            return await _attachmentAppService.GetImage(imageId);
        }
    }
}
