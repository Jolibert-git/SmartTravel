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
    public class DestinationRepository : GenericRepository<Destination>, IDestinationRepository
    {
        public DestinationRepository(DbContext context) : base(context) { }

        public IQueryable<Destination> GetByCountry(long countryId)
            => _dbSet.AsNoTracking()
                     .Include(d => d.Country)
                     .Where(d => d.IdCountry == countryId);

        public IQueryable<Destination> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(d => d.Country)
                     .Include(d => d.Hotels)
                     .Include(d => d.Airports);

        public IQueryable<Destination> SearchByCity(string city)
            => _dbSet.AsNoTracking()
                     .Include(d => d.Country)
                     .Where(d => d.City.Contains(city));
    }

}
