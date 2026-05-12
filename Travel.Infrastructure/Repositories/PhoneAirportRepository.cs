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
    public class PhoneAirportRepository : GenericRepository<PhoneAirport>, IPhoneAirportRepository
    {
        public PhoneAirportRepository(DbContext context) : base(context) { }

        public IQueryable<PhoneAirport> GetByAirport(long airportId)
            => _dbSet.AsNoTracking()
                     .Where(pa => pa.IdAirport == airportId);

        public async Task<bool> PhoneExistsAsync(long airportId, string phoneNumber, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(pa =>
                               pa.IdAirport == airportId &&
                               pa.PhoneNumber == phoneNumber,
                               cancellationToken);

        public async Task DeleteByAirportAsync(long airportId, CancellationToken cancellationToken = default)
        {
            var items = await _dbSet.Where(pa => pa.IdAirport == airportId)
                                    .ToListAsync(cancellationToken);
            _dbSet.RemoveRange(items);
        }
    }
}
