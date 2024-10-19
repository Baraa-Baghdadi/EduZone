using EduZone.Enum;
using EduZone.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Licenses
{
    public interface ILicenseRepository : IRepository<License,Guid>
    {
        Task<List<License>> GetListAsync(
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

        Task<License> GetLicenseAsync(Guid id);

    }
}
