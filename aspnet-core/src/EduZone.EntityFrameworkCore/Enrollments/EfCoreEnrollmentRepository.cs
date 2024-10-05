using EduZone.EntityFrameworkCore;
using EduZone.Instructors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EduZone.Enrollments
{
    public class EfCoreEnrollmentRepository : EfCoreRepository<EduZoneDbContext, Enrollment, Guid>, IEnrollmentRepository
    {
        public EfCoreEnrollmentRepository(IDbContextProvider<EduZoneDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Enrollment> GetByIdWithDetails(Guid id)
        {
            var query = (await GetQueryableAsync()).Include(r => r.Student).Include(r => r.Course).ThenInclude(r => r.Category);
            return await query.FirstOrDefaultAsync(row => row.Id == id);
        }

        public async Task<long> GetCountAsync(string? filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Student).Include(r => r.Course).ThenInclude(r => r.Category), filterText);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Enrollment>> GetListAsync(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Student).Include(r => r.Course).ThenInclude(r => r.Category), filterText);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EnrollmentConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsyncWithoutTenant(string? filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).IgnoreQueryFilters().Include(r => r.Student).Include(r => r.Course).ThenInclude(r => r.Category), filterText);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Enrollment>> GetListAsyncWithoutTenant(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).IgnoreQueryFilters().Include(r => r.Student).Include(r => r.Course).ThenInclude(r => r.Category), filterText);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EnrollmentConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Enrollment> ApplyFilter(
        IQueryable<Enrollment> query,
        string? filterText = null
        )
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Student.FirstName!.Contains(filterText!) || e.Student.LastName!.Contains(filterText!)
                || e.Student.Email!.Contains(filterText!) || e.Course.Title.Contains(filterText!) 
                || e.Course.Category.Name.Contains(filterText!));
        }
    }
}
