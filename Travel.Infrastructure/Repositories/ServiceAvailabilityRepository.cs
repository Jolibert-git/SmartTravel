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
    public class ServiceAvailabilityRepository : GenericRepository<ServiceAvailability>, IServiceAvailabilityRepository
    {
        public ServiceAvailabilityRepository(DbContext context) : base(context) { }

        public IQueryable<ServiceAvailability> GetByService(long serviceId)
            => _dbSet.AsNoTracking()
                     .Include(sa => sa.AvailabilityStatus)
                     .Where(sa => sa.IdService == serviceId)
                     .OrderByDescending(sa => sa.DateFrom);

        public async Task<bool> IsServiceAvailableAsync(long serviceId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
            => !await _dbSet.AsNoTracking()
                            .AnyAsync(sa =>
                                sa.IdService == serviceId &&
                                sa.DateFrom <= to &&
                                sa.DateTo >= from,
                                cancellationToken);

        public async Task<ServiceAvailability?> GetCurrentByServiceAsync(long serviceId, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .Include(sa => sa.AvailabilityStatus)
                           .FirstOrDefaultAsync(sa =>
                               sa.IdService == serviceId &&
                               sa.DateFrom <= DateTime.Now &&
                               sa.DateTo >= DateTime.Now,
                               cancellationToken);
    }

}
