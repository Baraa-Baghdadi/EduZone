using EduZone.Attachments;
using EduZone.Categories;
using EduZone.Courses;
using EduZone.Enrollments;
using EduZone.Instructors;
using EduZone.Lessons;
using EduZone.Reviewies;
using EduZone.Students;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using EduZone.Notifications;

namespace EduZone.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class EduZoneDbContext :
    AbpDbContext<EduZoneDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Notification> Notifications { get; set; }


    public EduZoneDbContext(DbContextOptions<EduZoneDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EduZoneConsts.DbTablePrefix + "YourEntities", EduZoneConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});


        builder.Entity<Attachment>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Attachments", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<Category>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Categories", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<Student>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Students", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<Instructor>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Instructors", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<Enrollment>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Enrollments", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
        });

        // For enable MultiTenancy in this table:
        builder.Entity<Enrollment>().HasQueryFilter(e => e.Course.Instructor.TenantId == CurrentTenant.Id);

        builder.Entity<Course>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Courses", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasMany(p => p.Lessons).WithOne().HasForeignKey(x => x.CourseId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Lesson>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Lessons", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<Course>().WithMany(x => x.Lessons).HasForeignKey(x => x.CourseId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Review>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Reviews", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Notification>(b =>
        {
            b.ToTable(EduZoneConsts.DbTablePrefix + "Notifications", EduZoneConsts.DbSchema);
            b.ConfigureByConvention();
        });

    }
}
