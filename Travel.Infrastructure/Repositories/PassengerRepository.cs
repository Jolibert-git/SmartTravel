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
    public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(DbContext context) : base(context) { }

        public async Task<Passenger?> GetByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .Include(p => p.DocumentType)
                           .Include(p => p.Country)
                           .FirstOrDefaultAsync(p => p.DocumentNumber == documentNumber, cancellationToken);

        public async Task<bool> DocumentExistsAsync(string documentNumber, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(p => p.DocumentNumber == documentNumber, cancellationToken);

        public IQueryable<Passenger> GetByReservation(long reservationId)
            => _dbSet.AsNoTracking()
                     .Include(p => p.DocumentType)
                     .Include(p => p.Country)
                     .Where(p => p.ReservationPassengers.Any(rp => rp.IdReservation == reservationId));

        public IQueryable<Passenger> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(p => p.DocumentType)
                     .Include(p => p.Country);
    }
}
