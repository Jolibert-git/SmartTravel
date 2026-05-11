using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;


namespace Travel.Domain.Contracts
{
    public interface IPayPalPaymentRepository
    {
        Task<PayPalPayment?> GetByOrderIdAsync(
        string orderId,
        CancellationToken ct = default);
    }
}
