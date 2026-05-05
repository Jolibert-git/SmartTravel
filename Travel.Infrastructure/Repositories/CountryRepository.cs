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
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DbContext context) : base(context) { }

        public async Task<Country?> GetByIsoCodeAsync(string isoCode, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(c => c.IsoCode == isoCode.ToUpper(), cancellationToken);

        public async Task<bool> IsoCodeExistsAsync(string isoCode, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(c => c.IsoCode == isoCode.ToUpper(), cancellationToken);
    }
}
