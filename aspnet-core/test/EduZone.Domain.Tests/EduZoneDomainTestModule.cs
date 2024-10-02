using Volo.Abp.Modularity;

namespace EduZone;

[DependsOn(
    typeof(EduZoneDomainModule),
    typeof(EduZoneTestBaseModule)
)]
public class EduZoneDomainTestModule : AbpModule
{

}
