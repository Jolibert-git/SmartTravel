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
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(DbContext context) : base(context) { }

        public IQueryable<Promotion> GetActive()
            => _dbSet.AsNoTracking()
                     .Include(p => p.DiscountType)
                     .Where(p =>
                         p.IsActive &&
                         p.DateFrom <= DateTime.Now &&
                         p.DateTo >= DateTime.Now)
                     .OrderBy(p => p.DateTo);

        public IQueryable<Promotion> GetByService(long serviceId)
            => _dbSet.AsNoTracking()
                     .Include(p => p.DiscountType)
                     .Where(p =>
                         p.IsActive &&
                         p.PromotionDetails.Any(pd => pd.IdService == serviceId));

        public IQueryable<Promotion> GetByPackage(long packageId)
            => _dbSet.AsNoTracking()
                     .Include(p => p.DiscountType)
                     .Where(p =>
                         p.IsActive &&
                         p.PromotionDetails.Any(pd => pd.IdPackage == packageId));

        public IQueryable<Promotion> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(p => p.DiscountType)
                     .Include(p => p.PromotionDetails)
                         .ThenInclude(pd => pd.OfferedService)
                     .Include(p => p.PromotionDetails)
                         .ThenInclude(pd => pd.Package);
    }



}
