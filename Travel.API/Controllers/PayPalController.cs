using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Application.Responses;

namespace Travel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[AllowAnonymous]
    public class PayPalController : ControllerBase
    {
        private readonly IPaypalService _paypalService;

        public PayPalController(IPaypalService paypalService)
        {
            _paypalService = paypalService;
        }

        
        [HttpPost("create-service/{id:long}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePayPalPaymentWithIdServicesAsync([FromRoute] long id, CancellationToken ct)
        {

            var payPalUrl = await _paypalService.CreateOrderFromServiceAsync(id, ct);

            return Ok(ApiResponse<object>.Ok(new { payPalUrl }, "Orden creada. Redirige al usuario a la URL."));
        }

       
        /// <summary>Crea una orden en PayPal y retorna la URL de aprobación.</summary>
        [HttpPost("create-order")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrder([FromBody] CreatePayPalOrderRequest request, CancellationToken ct)
        {
            var approvalUrl = await _paypalService.CreateOrderAsync(
                request.Amount,
                request.Currency,
                request.Description);

            return Ok(ApiResponse<object>.Ok(new { approvalUrl }, "Orden creada. Redirige al usuario a la URL."));
        }

        /// <summary>Captura el pago después de que el usuario aprueba en PayPal.</summary>
        [HttpPost("capture/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Capture(string orderId, CancellationToken ct)
        {
            var result = await _paypalService.CaptureOrderAsync(orderId);
            return Ok(ApiResponse<PayPalCaptureResponseDto>.Ok(result, "Pago capturado exitosamente."));
        }
    }
}
