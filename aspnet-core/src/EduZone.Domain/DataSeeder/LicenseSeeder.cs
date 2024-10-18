using EduZone.Categories;
using EduZone.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace EduZone.DataSeeder
{
    public class LicenseSeeder : ITransientDependency
    {
        private readonly IRepository<License> _licenseRepo;
        private readonly IGuidGenerator _guidGenerator;

        public LicenseSeeder(IRepository<License> licenseRepo, IGuidGenerator guidGenerator)
        {
            _licenseRepo = licenseRepo;
            _guidGenerator = guidGenerator;
        }

        public async Task Seed()
        {
            await CreateMainLicense();
        }

        private async Task CreateMainLicense()
        {
            if (!await _licenseRepo.AnyAsync())
            {
                List<License> licenses = new List<License>
                {
                    new License(_guidGenerator.Create(),"111111",DateTime.UtcNow.AddDays(1),false),
                    new License(_guidGenerator.Create(),"222222",DateTime.UtcNow.AddDays(1),false),
                    new License(_guidGenerator.Create(),"333333",DateTime.UtcNow.AddDays(1),false)
                };
                await _licenseRepo.InsertManyAsync(licenses, true);
            }
        }
    }
}
