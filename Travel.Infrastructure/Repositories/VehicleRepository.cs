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
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DbContext context) : base(context) { }

        public IQueryable<Vehicle> GetByDestination(long destinationId)
            => _dbSet.AsNoTracking()
                     .Include(v => v.OfferedService)
                     .Where(v => v.IdDestination == destinationId);

        public IQueryable<Vehicle> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(v => v.OfferedService)
                     .Include(v => v.Destination)
                         .ThenInclude(d => d.Country);

        public IQueryable<Vehicle> GetByTransmission(string transmission)
            => _dbSet.AsNoTracking()
                     .Where(v => v.Transmission == transmission);
    }

}
