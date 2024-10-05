using EduZone.Courses;
using EduZone.Enrollments;
using EduZone.Students;
using EduZone.UserNameFromToken;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Ratings
{
    [Authorize]
    public class RateAppService : EduZoneAppService, IRateAppService
    {
        private readonly IRateRepository _ratingRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IGetUserNameFromToken _getUserNameFromToken;
        private readonly IStudentRepository _studentRepository;

        public RateAppService(IRateRepository ratingRepository, ICourseRepository courseRepository, 
            IGetUserNameFromToken getUserNameFromToken, IStudentRepository studentRepository)
        {
            _ratingRepository = ratingRepository;
            _courseRepository = courseRepository;
            _getUserNameFromToken = getUserNameFromToken;
            _studentRepository = studentRepository;
        }

        public async Task<bool> RateCourse(Guid courseId,double rating)
        {
            var studentEmail = _getUserNameFromToken.GetEmailFromToken();
            var student = await _studentRepository.GetStudentByEmail(studentEmail);

            var course = await _courseRepository.FirstOrDefaultAsync(c => c.Id == courseId)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);

            var alreadyExist = await _ratingRepository.FirstOrDefaultAsync(r => r.CourseId == courseId && r.StudentId == student.Id);

            if (rating < 0 || rating > 5)
            {
                throw new UserFriendlyException(L[EduZoneDomainErrorCodes.ratingValidationValue]);
            }

            if (alreadyExist is not null)
            {
                alreadyExist.Value = rating;
                return true;
            }

            var newRate = new Rate(GuidGenerator.Create(),student.Id,courseId, rating);

            await _ratingRepository.InsertAsync(newRate,true);

            return true;

        }


        public async Task<PagedResultDto<RateDto>> GetRateMyCourse(GetCourseRatingOfInstructor input)
        {
            var totalCount = await _ratingRepository.GetCountAsync(input.FilterText);
            var items = await _ratingRepository.GetListAsync(input.FilterText,
                input.Sorting, input.MaxResultCount, input.SkipCount);
            var mappedItem = ObjectMapper.Map<List<Rate>, List<RateDto>>(items);

            foreach (var item in mappedItem)
            {
                item.Rate = (await _ratingRepository.GetListAsync()).Where(r => r.CourseId == item.CourseId).Select(r => r.Value).Average();
            }

            return new PagedResultDto<RateDto>
            {
                TotalCount = totalCount,
                Items = mappedItem
            };
        }


    }
}
