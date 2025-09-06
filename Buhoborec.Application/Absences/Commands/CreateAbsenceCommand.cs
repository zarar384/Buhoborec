using MediatR;
using System;

namespace Buhoborec.Application.Absences.Commands;

public record CreateAbsenceCommand(string UserId, DateTime StartDate, DateTime EndDate, string? Reason) : IRequest<Guid>;
