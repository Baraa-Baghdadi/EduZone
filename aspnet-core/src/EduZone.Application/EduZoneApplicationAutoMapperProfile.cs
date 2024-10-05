using AutoMapper;
using EduZone.Categories;
using EduZone.Courses;
using EduZone.Enrollments;
using EduZone.Instructors;
using EduZone.Lessons;
using EduZone.Localization;
using EduZone.Notifications;
using EduZone.Ratings;
using EduZone.Students;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Volo.Abp.DependencyInjection;

namespace EduZone;

public class EduZoneApplicationAutoMapperProfile : Profile,ISingletonDependency
{
    private readonly IStringLocalizer<EduZoneResource> L;
    public EduZoneApplicationAutoMapperProfile(IStringLocalizer<EduZoneResource> l)
    {
        L = l;
        var currentLanguage = CultureInfo.CurrentCulture;
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Instructor, InstructorDto>().ReverseMap();
        CreateMap<Lesson, LessonDto>().ReverseMap();
        CreateMap<Lesson, LessonDtoForAddCourse>().ReverseMap();
        CreateMap<Category,CategoryDto>().ReverseMap();
        CreateMap<Notification, InstructorNotificationDto>().ReverseMap();

        CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Student != null ? src.Student.FirstName : ""))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Student != null ? src.Student.LastName : ""))
            .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : ""))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Course.Category != null ? src.Course.Category.Name : ""))
            .ForMember(dest => dest.CourseIcon, opt => opt.MapFrom(src => src.Course != null ? src.Course.Icon : ""));

        CreateMap<Course, CourseDto>()
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""))
             .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.FirstName + " " + src.Instructor.LastName : ""));

        CreateMap<Rate, RateDto>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : ""))
            .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Course.Instructor != null ? src.Course.Instructor.FirstName + " " + src.Course.Instructor.LastName : ""));

    }
}
