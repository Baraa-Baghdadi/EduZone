using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EduZone.Lessons
{
    public interface ILessonAppService : IApplicationService
    {
        Task<List<LessonDto>> GetLessonsByCourseId(Guid id);
    }
}
