using MediatR;
using Buhoborec.Domain.Enums;
using System;

namespace Buhoborec.Application.WorkLogs.Commands;

public record LogWorkCommand(string UserId, WorkLogType Type, DateTime Timestamp) : IRequest<Guid>;
