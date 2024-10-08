﻿using EduZone.Data;
using EduZone.OpenIddict;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace EduZone.DataSeeder
{
    public class SeederService : ITransientDependency
    {
        private readonly EduZoneDbMigrationService _migrationService;
        private readonly OpenIddictDataSeedContributor _openIddictSeedContributer;
        private readonly CategorySeeder _categorySeeder;

        public SeederService(EduZoneDbMigrationService migrationService, OpenIddictDataSeedContributor openIddictSeedContributer,
            CategorySeeder categorySeeder)
        {
            _migrationService = migrationService;
            _openIddictSeedContributer = openIddictSeedContributer;
            _categorySeeder = categorySeeder;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync()
        {
            await _migrationService.MigrateAsync();
            var context = new DataSeedContext();
            await _openIddictSeedContributer.SeedAsync(context);

            // seed data:
            // await _categorySeeder.Seed();
        }
    }
}
