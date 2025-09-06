using MediatR;
using System;

namespace Buhoborec.Application.Tasks.Commands;

public record DeleteTaskCommand(Guid Id) : IRequest;
