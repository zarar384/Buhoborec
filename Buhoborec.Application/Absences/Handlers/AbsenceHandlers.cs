using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Buhoborec.Application.Absences.Commands;
using Buhoborec.Application.Absences.Queries;
using Buhoborec.Infrastructure.Persistence;
using Buhoborec.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Buhoborec.Application.Absences.Handlers;

public class CreateAbsenceHandler : IRequestHandler<CreateAbsenceCommand, Guid>
{
    private readonly AppDbContext _db;
    public CreateAbsenceHandler(AppDbContext db) { _db = db; }
    public async Task<Guid> Handle(CreateAbsenceCommand req, CancellationToken cancellationToken)
    {
        var a = new Absence { UserId = req.UserId, StartDate = req.StartDate.Date, EndDate = req.EndDate.Date, Reason = req.Reason };
        _db.Absences.Add(a);
        await _db.SaveChangesAsync(cancellationToken);
        return a.Id;
    }
}

public class DeleteAbsenceHandler : IRequestHandler<DeleteAbsenceCommand>
{
    private readonly AppDbContext _db;
    public DeleteAbsenceHandler(AppDbContext db) { _db = db; }
    public async Task<Unit> Handle(DeleteAbsenceCommand req, CancellationToken cancellationToken)
    {
        var a = await _db.Absences.FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (a != null) { _db.Absences.Remove(a); await _db.SaveChangesAsync(cancellationToken); }
        return Unit.Value;
    }
}

public class GetAbsencesHandler : IRequestHandler<GetAbsencesQuery, List<Absence>>
{
    private readonly AppDbContext _db;
    public GetAbsencesHandler(AppDbContext db) { _db = db; }
    public async Task<List<Absence>> Handle(GetAbsencesQuery req, CancellationToken cancellationToken)
    {
        //  var absencesDto = absences.Adapt<List<AbsenceDto>>();
        return await _db.Absences.AsNoTracking().OrderByDescending(x => x.StartDate).ToListAsync(cancellationToken);
    }
}
