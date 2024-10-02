using EduZone.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Students
{
    public interface IStudentRepository : IRepository<Student,Guid>
    {
        Task<List<Student>> GetListAsync(
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

        Task<Student> GetStudentByEmail(string email);
    }
}
