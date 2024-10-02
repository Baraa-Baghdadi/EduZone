using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EduZone.Data;

/* This is used if database provider does't define
 * IEduZoneDbSchemaMigrator implementation.
 */
public class NullEduZoneDbSchemaMigrator : IEduZoneDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
