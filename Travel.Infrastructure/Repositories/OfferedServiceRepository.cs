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
    public class OfferedServiceRepository : GenericRepository<OfferedService>, IOfferedServiceRepository
    {
        public OfferedServiceRepository(DbContext context) : base(context) { }

        public IQueryable<OfferedService> GetByType(long typeServiceId)
            => _dbSet.AsNoTracking()
                     .Where(s => s.IdTypeService == typeServiceId);

        public IQueryable<OfferedService> GetBySupplier(long supplierId)
            => _dbSet.AsNoTracking()
                     .Where(s => s.IdSupplier == supplierId);

        public IQueryable<OfferedService> GetAvailable(DateTime date)
            => _dbSet.AsNoTracking()
                     .Where(s => !s.ServiceAvailabilities
                         .Any(sa => sa.DateFrom <= date && sa.DateTo >= date))
                     .Include(s => s.TypeService)
                     .Include(s => s.Supplier);

        public IQueryable<OfferedService> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(s => s.TypeService)
                     .Include(s => s.Supplier)
                     .Include(s => s.ServiceAvailabilities)
                         .ThenInclude(sa => sa.AvailabilityStatus);

        public IQueryable<OfferedService> GetByPriceRange(decimal min, decimal max)
            => _dbSet.AsNoTracking()
                     .Where(s => s.Price >= min && s.Price <= max)
                     .OrderBy(s => s.Price);
    }
}
