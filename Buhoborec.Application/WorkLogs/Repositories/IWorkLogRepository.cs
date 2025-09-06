using Buhoborec.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Application.WorkLogs.Repositories
{
    public interface IWorkLogRepository
    {
        Task<Guid> AddAsync(WorkLog workLog, CancellationToken cancellationToken);
    }
}
