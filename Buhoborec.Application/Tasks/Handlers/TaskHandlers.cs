using Buhoborec.Application.Tasks.Commands;
using Buhoborec.Application.Tasks.Queries;
using Buhoborec.Application.Tasks.Repositories;
using Buhoborec.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Application.Tasks.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly ITaskRepository _repo;

        public CreateTaskHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateTaskCommand req, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Title = req.Title,
                Description = req.Description,
                AssignedTo = req.AssignedTo,
                DueDate = req.DueDate,
            };

            return await _repo.AddAsync(task, cancellationToken);
        }
    }

    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskRepository _repo;

        public UpdateTaskHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateTaskCommand req, CancellationToken cancellationToken)
        {
            var task = await _repo.GetByIdAsync(req.Id, cancellationToken);
            if (task == null) throw new Exception("Task not found");

            task.Title = req.Title;
            task.Description = req.Description;
            task.AssignedTo = req.AssignedTo;
            task.DueDate = req.DueDate;
            task.Status = req.Status;

            await _repo.UpdateAsync(task, cancellationToken);
            return Unit.Value;
        }
    }


    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _repo;

        public DeleteTaskHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteTaskCommand req, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(req.Id, cancellationToken);
            return Unit.Value;
        }
    }


    public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<TaskItem>>
    {
        private readonly ITaskRepository _repo;

        public GetTasksHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TaskItem>> Handle(GetTasksQuery req, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }
}