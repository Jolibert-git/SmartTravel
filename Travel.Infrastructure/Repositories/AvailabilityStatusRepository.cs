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
    public class AvailabilityStatusRepository : GenericRepository<AvailabilityStatus>, IAvailabilityStatusRepository
    {
        public AvailabilityStatusRepository(DbContext context) : base(context) { }

        public async Task<AvailabilityStatus?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(a => a.StatusDescription == description, cancellationToken);
    }
}
