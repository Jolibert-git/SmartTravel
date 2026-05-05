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
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(DbContext context) : base(context) { }

        public IQueryable<Package> GetActive()
            => _dbSet.AsNoTracking()
                     .Where(p => p.OfferStart <= DateTime.Now && p.OfferEnd >= DateTime.Now)
                     .OrderBy(p => p.OfferEnd);

        public IQueryable<Package> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(p => p.DetailPackages)
                         .ThenInclude(dp => dp.OfferedService)
                             .ThenInclude(s => s.TypeService);

        public IQueryable<Package> GetExpiring(int days)
        {
            var limit = DateTime.Now.AddDays(days);
            return _dbSet.AsNoTracking()
                         .Where(p => p.OfferEnd <= limit && p.OfferEnd >= DateTime.Now)
                         .OrderBy(p => p.OfferEnd);
        }
    }
}
