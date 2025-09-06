using Buhoborec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Application.Absences.Repositories
{
    public interface IAbsenceRepository
    {
        Task<Guid> AddAsync(Absence absence, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Absence>> GetAllAsync(CancellationToken cancellationToken);
    }
}
