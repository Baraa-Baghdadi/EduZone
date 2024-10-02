using EduZone.Enrollments;
using EduZone.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduZone.Courses
{
    public class EfCoreCourseRepository : EfCoreRepository<EduZoneDbContext, Course, Guid>, ICourseRepository
    {
        public EfCoreCourseRepository(IDbContextProvider<EduZoneDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string? filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Instructor), filterText);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Course>> GetListAsync(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Category).Include(r => r.Instructor), filterText);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CourseConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<Course> GetCourseById(Guid id)
        {
            var query = (await GetQueryableAsync()).Include(r => r.Category).Include(r => r.Instructor).Include(r => r.Lessons);
            return await query.FirstOrDefaultAsync(row => row.Id == id);
        }

        public async Task<long> GetCountWithoutTenantAsync(string? filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Instructor), filterText).IgnoreQueryFilters();
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Course>> GetListWithoutTenantAsync(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Category).Include(r => r.Instructor), filterText).IgnoreQueryFilters();
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CourseConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }



        protected virtual IQueryable<Course> ApplyFilter(
        IQueryable<Course> query,
        string? filterText = null
        )
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Title!.Contains(filterText!) 
                || e.Description!.Contains(filterText!));
        }

    }
}
