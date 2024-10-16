using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace EduZone.DataSeeder
{
    public class AdminHostSeederContributer : ITransientDependency
    {
        private readonly IDataSeeder _dataSeeder;

        public AdminHostSeederContributer(IDataSeeder dataSeeder)
        {
            _dataSeeder = dataSeeder;
        }

        public async Task HostAdminSeedAsync()
        {
            await _dataSeeder.SeedAsync(new DataSeedContext(null)
                .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName,
                EduZoneConsts.AdminEmailDefaultValue)
                .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName,
                EduZoneConsts.AdminPasswordDefaultValue)
                );
        }
    }
}
