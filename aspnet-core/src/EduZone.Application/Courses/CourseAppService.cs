using EduZone.Attachments;
using EduZone.Categories;
using EduZone.Enrollments;
using EduZone.Instructors;
using EduZone.Lessons;
using EduZone.Permissions;
using EduZone.UserNameFromToken;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace EduZone.Courses
{
    [Authorize]
    public class CourseAppService : EduZoneAppService, ICourseAppService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IGetUserNameFromToken _getUserNameFromToken;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IAttachmentAppService _attachmentAppService;
        private readonly IBlobContainer _blobContainer;

        public CourseAppService(ICourseRepository courseRepository, IGetUserNameFromToken getUserNameFromToken,
            IRepository<Category> categoryRepository, 
            IRepository<Instructor> instructorRepository, IAttachmentAppService attachmentAppService,
            IBlobContainer blobContainer)
        {
            _courseRepository = courseRepository;
            _getUserNameFromToken = getUserNameFromToken;
            _categoryRepository = categoryRepository;
            _instructorRepository = instructorRepository;
            _attachmentAppService = attachmentAppService;
            _blobContainer = blobContainer;
        }



        // For Admin:
        [Authorize(EduZonePermissions.AllCourses.View)]
        public async Task<PagedResultDto<CourseDto>> GetAllCourses(GetCoursesInput input)
        {
            var totalCount = await _courseRepository.GetCountWithoutTenantAsync(input.FilterText);
            var items = await _courseRepository.GetListWithoutTenantAsync(input.FilterText,
                input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<CourseDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Course>, List<CourseDto>>(items)
            };
        }


        // For Instructor:
        [Authorize(EduZonePermissions.Courses.View)]
        public async Task<PagedResultDto<CourseDto>> GetMyCourses(GetCoursesInput input)
        {
            var totalCount = await _courseRepository.GetCountAsync(input.FilterText);
            var items = await _courseRepository.GetListAsync(input.FilterText,
                input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<CourseDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Course>, List<CourseDto>>(items)
            };
        }


        [Authorize(EduZonePermissions.Courses.Create)]
        public async Task<CourseDto> CreateNewCourse(NewCourseInput input)
        {
            var instructorEmail =  _getUserNameFromToken.GetEmailFromToken();
            var instructor = await _instructorRepository.FirstOrDefaultAsync(i => i.Email == instructorEmail)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);


            var dbCategory = await _categoryRepository.GetAsync(row => row.Id == input.CategoryId)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);

            // Convert From LessonDto to Lesson:
            var lessons = ObjectMapper.Map<List<LessonDtoForAddCourse>, List<Lesson>>(input.Lessons);
            lessons.ForEach(x => x.SetId(GuidGenerator.Create()));

            // Generate thumbnail:
            var newImage = new AttachmentCreateDto()
            {
                Blop = input.Blop,
                FileName = input.FileName,
                FileType = input.FileType,
                FileSize = input.FileSize
            };

            // Create Image in Attachment Table For Get Orginal Image:
            var orginalImage = await _attachmentAppService.CreateAttachmentAsync(newImage);

            // Create New Course:
            var course = new Course(GuidGenerator.Create(), input.Title, input.Description, input.Price, input.NewPrice, ServiceHelper.GetThumbNail(input.Blop)
                , orginalImage.Id, input.CategoryId,instructor.Id, lessons);
            await _courseRepository.InsertAsync(course, true);
            
            return ObjectMapper.Map<Course,CourseDto>(course);
        }

        [Authorize(EduZonePermissions.Courses.View)]
        public async Task<CourseDto> GetCourseById(Guid id)
        {
            var course = await _courseRepository.GetCourseById(id)
                ?? throw new UserFriendlyException(L[EduZoneDomainErrorCodes.NotFound]);
            // For show image as it in FE:
            var mappingData = ObjectMapper.Map<Course, CourseDto>(course);
            mappingData.OrginalImage = Convert.ToBase64String(await _attachmentAppService.GetImage(course.ImageId));
            return mappingData;
        }
    }
}
