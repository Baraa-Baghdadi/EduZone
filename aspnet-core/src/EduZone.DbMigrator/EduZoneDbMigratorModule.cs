using EduZone.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EduZone.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EduZoneEntityFrameworkCoreModule),
    typeof(EduZoneApplicationContractsModule)
    )]
public class EduZoneDbMigratorModule : AbpModule
{
}
