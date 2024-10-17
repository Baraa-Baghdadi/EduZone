using EduZone.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Certificates
{
    public interface ICertificateRepository : IRepository<Certificate,Guid>
    {
        Task<List<Certificate>> GetListAsync(
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

        Task<Certificate> GetCertificateById(Guid id);
    }
}
