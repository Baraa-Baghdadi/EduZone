using EduZone.EntityFrameworkCore;
using EduZone.Enum;
using EduZone.Instructors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace EduZone.Licenses
{
    public class EfCoreLicenseRepository : EfCoreRepository<EduZoneDbContext, License, Guid>, ILicenseRepository
    {
        public EfCoreLicenseRepository(IDbContextProvider<EduZoneDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string? filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<License> GetLicenseAsync(Guid id)
        {
            var query = await GetQueryableAsync();
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<License>> GetListAsync(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LicenseConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<License> ApplyFilter(
        IQueryable<License> query,
        string? filterText = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Key!.Contains(filterText!));
        }
    }
}
