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
    public class DetailPackageRepository : GenericRepository<DetailPackage>, IDetailPackageRepository
    {
        public DetailPackageRepository(DbContext context) : base(context) { }

        public IQueryable<DetailPackage> GetByPackage(long packageId)
            => _dbSet.AsNoTracking()
                     .Include(dp => dp.OfferedService)
                     .Where(dp => dp.IdPackage == packageId);

        public async Task DeleteByPackageAsync(long packageId, CancellationToken cancellationToken = default)
        {
            var items = await _dbSet.Where(dp => dp.IdPackage == packageId)
                                    .ToListAsync(cancellationToken);
            _dbSet.RemoveRange(items);
        }
    }
}
