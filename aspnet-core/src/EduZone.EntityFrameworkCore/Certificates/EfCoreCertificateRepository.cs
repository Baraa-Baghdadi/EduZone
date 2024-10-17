using EduZone.Courses;
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


namespace EduZone.Certificates
{
    public class EfCoreCertificateRepository : EfCoreRepository<EduZoneDbContext, Certificate, Guid>, ICertificateRepository
    {
        public EfCoreCertificateRepository(IDbContextProvider<EduZoneDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Certificate> GetCertificateById(Guid id)
        {
            var query = (await GetQueryableAsync()).Include(r => r.Course).ThenInclude(r => r.Instructor).Include(r => r.Student);
            return await query.FirstOrDefaultAsync(row => row.Id == id);
        }

        public async Task<long> GetCountAsync(string? filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Course).ThenInclude(r => r.Instructor).Include(r => r.Student), filterText);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }


        public async Task<List<Certificate>> GetListAsync(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()).Include(r => r.Course).ThenInclude(r => r.Instructor).Include(r => r.Student), filterText);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CourseConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Certificate> ApplyFilter(
        IQueryable<Certificate> query,
        string? filterText = null
        )
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Student.FirstName!.Contains(filterText!)
                || e.Student.LastName!.Contains(filterText!) || (e.Student.FirstName +" " + e.Student.LastName).Contains(filterText!)
                || e.Course.Title!.Contains(filterText!));
        }
    }
}
