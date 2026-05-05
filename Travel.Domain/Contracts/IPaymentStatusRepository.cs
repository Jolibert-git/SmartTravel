using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPaymentStatusRepository : IGenericRepository<PaymentStatus>
    {
        /// <summary>Busca un estado de pago por descripción.</summary>
        Task<PaymentStatus?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default);
    }
}
