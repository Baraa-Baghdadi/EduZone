using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EduZone.Attachments
{
  public interface IAttachmentAppService : IApplicationService
    {
    Task<byte[]> GetImage(Guid? imageId);
    Task<AttachmentDto> CreateAttachmentAsync(AttachmentCreateDto input);
    Task DeleteImage(Guid imageId);
  }
}
