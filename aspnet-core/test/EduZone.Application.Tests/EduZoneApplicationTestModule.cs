using Volo.Abp.Modularity;

namespace EduZone;

[DependsOn(
    typeof(EduZoneApplicationModule),
    typeof(EduZoneDomainTestModule)
)]
public class EduZoneApplicationTestModule : AbpModule
{

}
