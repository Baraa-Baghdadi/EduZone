using Volo.Abp.Modularity;

namespace EduZone;

/* Inherit from this class for your domain layer tests. */
public abstract class EduZoneDomainTestBase<TStartupModule> : EduZoneTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
