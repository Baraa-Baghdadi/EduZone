using Volo.Abp.Modularity;

namespace EduZone;

public abstract class EduZoneApplicationTestBase<TStartupModule> : EduZoneTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
