using Buhoborec.Application.Tasks.Repositories;
using Buhoborec.Domain.Entities;
using Buhoborec.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Infrastructure.Tasks.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> AddAsync(TaskItem task, CancellationToken cancellationToken)
        {
            _db.TaskItems.Add(task);
            await _db.SaveChangesAsync(cancellationToken);
            return task.Id;
        }

        public async Task UpdateAsync(TaskItem task, CancellationToken cancellationToken)
        {
            var existing = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == task.Id, cancellationToken);
            if (existing == null) throw new Exception("Task not found");

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.AssignedTo = task.AssignedTo;
            existing.DueDate = task.DueDate;
            existing.Status = task.Status;

            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (task != null)
            {
                _db.TaskItems.Remove(task);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _db.TaskItems
                .AsNoTracking()
                .OrderByDescending(x => x.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
