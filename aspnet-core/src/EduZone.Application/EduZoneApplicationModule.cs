using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Localization;

namespace EduZone;

[DependsOn(
    typeof(EduZoneDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(EduZoneApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpLocalizationModule),
    typeof(AbpBlobStoringModule),
    typeof(AbpTenantManagementApplicationModule)
    )]
[DependsOn(typeof(AbpBlobStoringFileSystemModule))]
    public class EduZoneApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // For localiztion in mapper:
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.Configurators.Add(AbpAutoMapperConfigurationContext =>
            {
                var EduZoneApplicationAutoMapperProfile = AbpAutoMapperConfigurationContext.ServiceProvider
                .GetRequiredService<EduZoneApplicationAutoMapperProfile>();

                AbpAutoMapperConfigurationContext.MapperConfiguration
                .AddProfile(EduZoneApplicationAutoMapperProfile);
            });
        });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });

        //Configure<AbpAutoMapperOptions>(options =>
        //{
        //    options.AddMaps<EduZoneApplicationModule>();
        //});

        // For Blob:
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.IsMultiTenant = false;
                container.UseDatabase();
            });
        });
    }
}
