using MediatR;
using System;

namespace Buhoborec.Application.Absences.Commands;

public record DeleteAbsenceCommand(Guid Id) : IRequest;
