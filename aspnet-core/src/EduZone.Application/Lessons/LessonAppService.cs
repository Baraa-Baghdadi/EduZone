using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Lessons
{
    public class LessonAppService : EduZoneAppService, ILessonAppService
    {
        private readonly IRepository<Lesson> _lessonsRepository;

        public LessonAppService(IRepository<Lesson> lessonsRepository)
        {
            _lessonsRepository = lessonsRepository;
        }

        public async Task<List<LessonDto>> GetLessonsByCourseId(Guid id)
        {
            var lessons = (await _lessonsRepository.GetQueryableAsync()).Where(r => r.CourseId == id).ToList();
            var mappingData = ObjectMapper.Map<List<Lesson>, List<LessonDto>>(lessons);
            return mappingData;
        }
    }
}
