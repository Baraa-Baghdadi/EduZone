using EduZone.Enum;
using EduZone.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Enrollments
{
    public interface IEnrollmentRepository : IRepository<Enrollment,Guid>
    {
        Task<List<Enrollment>> GetListAsync(
        string? filterText = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );
        Task<long> GetCountAsync(
        string? filterText = null,
        CancellationToken cancellationToken = default
        );

        Task<Enrollment> GetByIdWithDetails( Guid id );

        #region mobile
        Task<List<Enrollment>> GetListAsyncWithoutTenant(
        string? filterText = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsyncWithoutTenant(
        string? filterText = null,
        CancellationToken cancellationToken = default
        );

        #endregion
    }
}
