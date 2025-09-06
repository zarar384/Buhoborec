using Buhoborec.Application.Absences.Commands;
using Buhoborec.Application.Absences.Queries;
using Buhoborec.Application.Absences.Repositories;
using Buhoborec.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Application.Absences.Handlers
{
    public class CreateAbsenceHandler : IRequestHandler<CreateAbsenceCommand, Guid>
    {
        private readonly IAbsenceRepository _repo;

        public CreateAbsenceHandler(IAbsenceRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateAbsenceCommand req, CancellationToken cancellationToken)
        {
            var absence = new Absence
            {
                UserId = req.UserId,
                StartDate = req.StartDate.Date,
                EndDate = req.EndDate.Date,
                Reason = req.Reason
            };

            return await _repo.AddAsync(absence, cancellationToken);
        }
    }


    public class DeleteAbsenceHandler : IRequestHandler<DeleteAbsenceCommand>
    {
        private readonly IAbsenceRepository _repo;

        public DeleteAbsenceHandler(IAbsenceRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteAbsenceCommand req, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(req.Id, cancellationToken);
            return Unit.Value;
        }
    }

    public class GetAbsencesHandler : IRequestHandler<GetAbsencesQuery, List<Absence>>
    {
        private readonly IAbsenceRepository _repo;

        public GetAbsencesHandler(IAbsenceRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Absence>> Handle(GetAbsencesQuery req, CancellationToken cancellationToken)
        {
            //  var absencesDto = absences.Adapt<List<AbsenceDto>>();
            return await _repo.GetAllAsync(cancellationToken);
        }
    }
}