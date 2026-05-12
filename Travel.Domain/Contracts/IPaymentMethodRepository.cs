using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod>
    {
        /// <summary>IQueryable de métodos de pago activos.</summary>
        IQueryable<PaymentMethod> GetActive();
    }
}
