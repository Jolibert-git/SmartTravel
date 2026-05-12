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
    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        public AirportRepository(DbContext context) : base(context) { }

        public async Task<Airport?> GetByIataCodeAsync(string iataCode, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(a => a.CodeIata == iataCode.ToUpper(), cancellationToken);

        public IQueryable<Airport> GetByDestination(long destinationId)
            => _dbSet.AsNoTracking()
                     .Where(a => a.IdDestination == destinationId);

        public IQueryable<Airport> GetWithFlights()
            => _dbSet.AsNoTracking()
                     .Include(a => a.DepartureFlights)
                     .Include(a => a.ArrivalFlights);
    }

}
