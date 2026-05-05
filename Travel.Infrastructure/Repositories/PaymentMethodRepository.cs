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
    public class PaymentMethodRepository : GenericRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(DbContext context) : base(context) { }

        public IQueryable<PaymentMethod> GetActive()
            => _dbSet.AsNoTracking()
                     .Where(pm => pm.IsActive);
    }

}
