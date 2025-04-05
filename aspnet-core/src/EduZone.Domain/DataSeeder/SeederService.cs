using EduZone.Data;
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
        private readonly AdminHostSeederContributer _adminHostSeederContributer;
        private readonly CategorySeeder _categorySeeder;
        private readonly LicenseSeeder _licenseSeeder;
        private readonly CountryCodesSeeder _countryCodesSeeder;

        public SeederService(EduZoneDbMigrationService migrationService, OpenIddictDataSeedContributor openIddictSeedContributer,
            AdminHostSeederContributer adminHostSeederContributer, CategorySeeder categorySeeder, LicenseSeeder licenseSeeder, CountryCodesSeeder countryCodesSeeder)
        {
            _migrationService = migrationService;
            _openIddictSeedContributer = openIddictSeedContributer;
            _adminHostSeederContributer = adminHostSeederContributer;
            _categorySeeder = categorySeeder;
            _licenseSeeder = licenseSeeder;
            _countryCodesSeeder = countryCodesSeeder;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync()
        {
            await _migrationService.MigrateAsync();
            var context = new DataSeedContext();
            await _openIddictSeedContributer.SeedAsync(context);

            // admin seed:
            await _adminHostSeederContributer.HostAdminSeedAsync();

            // seed data:
            //await _categorySeeder.Seed();

            // seed licenses:
            //await _licenseSeeder.Seed();

            // seed countryCode:
            //await _countryCodesSeeder.Seed();
        }
    }
}
