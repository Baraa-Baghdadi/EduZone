using System.Threading.Tasks;

namespace EduZone.Data;

public interface IEduZoneDbSchemaMigrator
{
    Task MigrateAsync();
}
