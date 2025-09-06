using MediatR;
using System;

namespace Buhoborec.Application.Tasks.Commands;

public record CreateTaskCommand(string Title, string? Description, string? AssignedTo, DateTime? DueDate) : IRequest<Guid>;
