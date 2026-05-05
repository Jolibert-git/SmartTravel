using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Contracts
{
    public interface IPaypalService
    {
        Task<string> CreatePaymentAsync(decimal amount, string currency, string description);
    }
}
