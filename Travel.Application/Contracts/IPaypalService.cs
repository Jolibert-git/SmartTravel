using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;

namespace Travel.Application.Contracts
{
    public interface IPaypalService
    {
        Task<string> CreateOrderFromServiceAsync(long id, CancellationToken ct);
        Task<string> CreatePaymentAsync(decimal amount, string currency, string description);

        Task<string> GetAccessTokenAsync();

        Task<string> CreateOrderAsync(decimal amount,string currency,string description );

        Task<PayPalCaptureResponseDto> CaptureOrderAsync(string orderId);
    }
}
