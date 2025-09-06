using Buhoborec.Application.Common.Interfaces;
using Buhoborec.Application.WorkLogs.Commands;
using Buhoborec.Application.WorkLogs.Repositories;
using Buhoborec.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Application.WorkLogs.Handlers
{
    public class LogWorkHandler : IRequestHandler<LogWorkCommand, Guid>
    {
        private readonly IWorkLogRepository _repo;
        private readonly IDateTime _dateTime;

        public LogWorkHandler(IWorkLogRepository repo, IDateTime dateTime)
        {
            _repo = repo;
            _dateTime = dateTime;
        }

        public async Task<Guid> Handle(LogWorkCommand request, CancellationToken cancellationToken)
        {
            var workLog = new WorkLog
            {
                UserId = request.UserId,
                Type = request.Type,
                Timestamp = request.Timestamp.ToUniversalTime()
            };

            return await _repo.AddAsync(workLog, cancellationToken);
        }
    }
}