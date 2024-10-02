using System;
using System.Collections.Generic;
using System.Text;

namespace EduZone.Attachments
{
  public class AttachmentDto
  {
    public Guid Id { get; set; }
    public string BlopName { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
    public int FileSize { get; set; }
  }
}
