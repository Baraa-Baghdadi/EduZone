using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using EduZone.Instructors;
using EduZone.Students;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.BlobStoring.FileSystem;
using EduZone.Enrollments;
using EduZone.Courses;
using EduZone.Notifications;
using EduZone.Ratings;

namespace EduZone.EntityFrameworkCore;

[DependsOn(
    typeof(EduZoneDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
[DependsOn(typeof(BlobStoringDatabaseEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpBlobStoringFileSystemModule))]
    public class EduZoneEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        EduZoneEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<EduZoneDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Student, EfCoreStudentRepository>();
            options.AddRepository<Instructor, EfCoreInstructorRepository>();
            options.AddRepository<Enrollment, EfCoreEnrollmentRepository>();
            options.AddRepository<Course, EfCoreCourseRepository>();
            options.AddRepository<Notification, EfCoreNotificationRepository>();
            options.AddRepository<Rate, EfCoreRateRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also EduZoneMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }
}
