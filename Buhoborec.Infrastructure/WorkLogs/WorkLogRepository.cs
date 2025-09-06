using Buhoborec.Application.WorkLogs.Repositories;
using Buhoborec.Domain.Entities;
using Buhoborec.Infrastructure.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Infrastructure.WorkLogs
{
    public class WorkLogRepository : IWorkLogRepository
    {
        private readonly AppDbContext _db;

        public WorkLogRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> AddAsync(WorkLog workLog, CancellationToken cancellationToken)
        {
            _db.WorkLogs.Add(workLog);
            await _db.SaveChangesAsync(cancellationToken);
            return workLog.Id;
        }
    }
}
