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

namespace EduZone.Ratings
{
    public class EfCoreRateRepository : EfCoreRepository<EduZoneDbContext, Rate, Guid>, IRateRepository
    {
        public EfCoreRateRepository(IDbContextProvider<EduZoneDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string? filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Student).Include(r => r.Course).ThenInclude(r => r.Category), filterText);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Rate>> GetListAsync(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Student).Include(r => r.Course).ThenInclude(r => r.Instructor).Include(r=>r.Course.Category), filterText);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RateConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Rate> ApplyFilter(
        IQueryable<Rate> query,
        string? filterText = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Course.Title.Contains(filterText!) 
                    || e.Course.Category.Name.Contains(filterText!));
        }
    }
}
