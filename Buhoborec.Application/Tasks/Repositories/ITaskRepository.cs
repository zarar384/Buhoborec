using Buhoborec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Application.Tasks.Repositories
{
    public interface ITaskRepository
    {
        Task<Guid> AddAsync(TaskItem task, CancellationToken cancellationToken);
        Task UpdateAsync(TaskItem task, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
