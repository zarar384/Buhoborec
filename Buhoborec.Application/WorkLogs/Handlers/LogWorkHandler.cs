using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Buhoborec.Application.WorkLogs.Commands;
using Buhoborec.Application.Common.Interfaces;
using Buhoborec.Domain.Entities;
using Buhoborec.Infrastructure.Persistence;
using System;

namespace Buhoborec.Application.WorkLogs.Handlers;

public class LogWorkHandler : IRequestHandler<LogWorkCommand, Guid>
{
    private readonly AppDbContext _db;
    private readonly IDateTime _dateTime;

    public LogWorkHandler(AppDbContext db, IDateTime dateTime)
    {
        _db = db;
        _dateTime = dateTime;
    }

    public async Task<Guid> Handle(LogWorkCommand request, CancellationToken cancellationToken)
    {
        var wl = new WorkLog
        {
            UserId = request.UserId,
            Type = request.Type,
            Timestamp = request.Timestamp.ToUniversalTime()
        };
        _db.WorkLogs.Add(wl);
        await _db.SaveChangesAsync(cancellationToken);
        return wl.Id;
    }
}
