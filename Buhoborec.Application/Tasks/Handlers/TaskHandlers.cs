using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Buhoborec.Application.Tasks.Commands;
using Buhoborec.Application.Tasks.Queries;
using Buhoborec.Infrastructure.Persistence;
using Buhoborec.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Buhoborec.Application.Tasks.Handlers;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly AppDbContext _db;
    public CreateTaskHandler(AppDbContext db) { _db = db; }
    public async Task<Guid> Handle(CreateTaskCommand req, CancellationToken cancellationToken)
    {
        var t = new TaskItem { Title = req.Title, Description = req.Description, AssignedTo = req.AssignedTo, DueDate = req.DueDate };
        _db.TaskItems.Add(t);
        await _db.SaveChangesAsync(cancellationToken);
        return t.Id;
    }
}

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly AppDbContext _db;
    public UpdateTaskHandler(AppDbContext db) { _db = db; }
    public async Task<Unit> Handle(UpdateTaskCommand req, CancellationToken cancellationToken)
    {
        var t = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (t == null) throw new Exception("Task not found");
        t.Title = req.Title;
        t.Description = req.Description;
        t.AssignedTo = req.AssignedTo;
        t.DueDate = req.DueDate;
        t.Status = req.Status;
        await _db.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly AppDbContext _db;
    public DeleteTaskHandler(AppDbContext db) { _db = db; }
    public async Task<Unit> Handle(DeleteTaskCommand req, CancellationToken cancellationToken)
    {
        var t = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (t != null) { _db.TaskItems.Remove(t); await _db.SaveChangesAsync(cancellationToken); }
        return Unit.Value;
    }
}

public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<TaskItem>>
{
    private readonly AppDbContext _db;
    public GetTasksHandler(AppDbContext db) { _db = db; }
    public async Task<List<TaskItem>> Handle(GetTasksQuery req, CancellationToken cancellationToken)
    {
        return await _db.TaskItems.AsNoTracking().OrderByDescending(x => x.DueDate).ToListAsync(cancellationToken);
    }
}
