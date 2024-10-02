using EduZone.Enrollments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Courses
{
    public interface ICourseRepository : IRepository<Course,Guid> 
    {
        #region instructor
        Task<List<Course>> GetListAsync(
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

        Task<Course> GetCourseById(Guid id);

        #endregion

        #region Admin
        Task<List<Course>> GetListWithoutTenantAsync(
        string? filterText = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
        );

        Task<long> GetCountWithoutTenantAsync(
        string? filterText = null,
        CancellationToken cancellationToken = default
        );

        #endregion

    }
}
