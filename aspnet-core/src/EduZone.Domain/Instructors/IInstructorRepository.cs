using EduZone.Enum;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Instructors
{
    public interface IInstructorRepository : IRepository<Instructor, Guid>
    {
        Task<List<Instructor>> GetListAsync(
        string? filterText = null,
        Gender? gender = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
        );
        Task<long> GetCountAsync(
        string? filterText = null,
        Gender? gender = null,
        CancellationToken cancellationToken = default
        );

        Task<Instructor> GetInstructorByEmail(string email);
    }
}
