using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;
using Travel.Domain.Contracts;

namespace Travel.Infrastructure.Repositories
{
    public class ReservationStatusRepository : GenericRepository<ReservationStatus>, IReservationStatusRepository
    {
        public ReservationStatusRepository(DbContext context) : base(context) { }

        public async Task<ReservationStatus?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(rs => rs.StatusDescription == description, cancellationToken);
    }
}
