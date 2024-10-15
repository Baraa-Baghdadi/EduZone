using EduZone.EntityFrameworkCore;
using EduZone.Enum;
using EduZone.Students;
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

namespace EduZone.Instructors
{
    public class EfCoreInstructorRepository : EfCoreRepository<EduZoneDbContext, Instructor, Guid>, IInstructorRepository
    {
        public EfCoreInstructorRepository(IDbContextProvider<EduZoneDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string? filterText = null, Gender? gender = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, gender);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

  
        public async Task<List<Instructor>> GetListAsync(string? filterText = null, Gender? gender = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, gender);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? InstructorConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<Instructor> GetInstructorByEmail(string email)
        {
            var query = (await GetQueryableAsync());
            return await query.FirstOrDefaultAsync(row => row.Email == email);
        }


        protected virtual IQueryable<Instructor> ApplyFilter(
        IQueryable<Instructor> query,
        string? filterText = null,
         Gender? gender = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FirstName!.Contains(filterText!) || e.LastName!.Contains(filterText!)  || e.Email!.Contains(filterText!))
                .WhereIf(gender.HasValue, e => e.Gender == gender);
        }
    }
}
