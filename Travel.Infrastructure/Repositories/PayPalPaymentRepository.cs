using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Persistence.Context;

namespace Travel.Infrastructure.Repositories
{
    public class PayPalPaymentRepository : GenericRepository<PayPalPayment>,
      IPayPalPaymentRepository
    {
        public PayPalPaymentRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<PayPalPayment?> GetByOrderIdAsync(
            string orderId,
            CancellationToken ct = default)
        {
            return await _dbSet
                .Include(p => p.Payment)
                .FirstOrDefaultAsync(
                    p => p.PayPalOrderId == orderId,
                    ct);
        }
    }
}
