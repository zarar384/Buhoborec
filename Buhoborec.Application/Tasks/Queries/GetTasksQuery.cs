using MediatR;
using System.Collections.Generic;
using Buhoborec.Domain.Entities;

namespace Buhoborec.Application.Tasks.Queries;

public record GetTasksQuery() : IRequest<List<TaskItem>>;
