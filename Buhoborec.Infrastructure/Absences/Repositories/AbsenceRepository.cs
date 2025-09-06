using Buhoborec.Application.Absences.Repositories;
using Buhoborec.Domain.Entities;
using Buhoborec.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Infrastructure.Absences.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly AppDbContext _db;

        public AbsenceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> AddAsync(Absence absence, CancellationToken cancellationToken)
        {
            _db.Absences.Add(absence);
            await _db.SaveChangesAsync(cancellationToken);
            return absence.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var a = await _db.Absences.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (a != null)
            {
                _db.Absences.Remove(a);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Absence>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _db.Absences
                .AsNoTracking()
                .OrderByDescending(x => x.StartDate)
                .ToListAsync(cancellationToken);
        }
    }
}
