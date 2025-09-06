using MediatR;
using System;

namespace Buhoborec.Application.Tasks.Commands;

public record UpdateTaskCommand(Guid Id, string Title, string? Description, string? AssignedTo, DateTime? DueDate, string Status) : IRequest;
