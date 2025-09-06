using MediatR;
using System.Collections.Generic;
using Buhoborec.Domain.Entities;

namespace Buhoborec.Application.Absences.Queries;

public record GetAbsencesQuery() : IRequest<List<Absence>>;
