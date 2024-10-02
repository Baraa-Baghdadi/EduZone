using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EduZone.Data;
using Volo.Abp.DependencyInjection;

namespace EduZone.EntityFrameworkCore;

public class EntityFrameworkCoreEduZoneDbSchemaMigrator
    : IEduZoneDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreEduZoneDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the EduZoneDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<EduZoneDbContext>()
            .Database
            .MigrateAsync();
    }
}
