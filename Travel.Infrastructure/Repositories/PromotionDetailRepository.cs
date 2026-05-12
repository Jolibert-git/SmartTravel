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
    public class PromotionDetailRepository : GenericRepository<PromotionDetail>, IPromotionDetailRepository
    {
        public PromotionDetailRepository(DbContext context) : base(context) { }

        public IQueryable<PromotionDetail> GetByPromotion(long promotionId)
            => _dbSet.AsNoTracking()
                     .Include(pd => pd.OfferedService)
                     .Include(pd => pd.Package)
                     .Where(pd => pd.IdPromotion == promotionId);
    }

}
