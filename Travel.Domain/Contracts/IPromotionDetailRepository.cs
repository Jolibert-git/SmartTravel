using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPromotionDetailRepository : IGenericRepository<PromotionDetail>
    {
        /// <summary>IQueryable de detalles de una promoción.</summary>
        IQueryable<PromotionDetail> GetByPromotion(long promotionId);
    }
}
