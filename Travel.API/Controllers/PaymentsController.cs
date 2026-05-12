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
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<PaymentResponseDto>.Ok(await _paymentService.GetByIdAsync(id, ct)));

        [HttpGet("by-reservation/{reservationId:long}")]
        public async Task<IActionResult> GetByReservation(long reservationId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _paymentService.GetByReservationAsync(reservationId, ct)));

        [HttpGet("by-reservation/{reservationId:long}/total")]
        public async Task<IActionResult> GetTotalPaid(long reservationId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _paymentService.GetTotalPaidAsync(reservationId, ct)));

        [HttpGet("by-date-range")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByDateRange(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _paymentService.GetByDateRangeAsync(from, to, ct)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request, CancellationToken ct)
        {
            var result = await _paymentService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<PaymentResponseDto>.Created(result, "Pago registrado exitosamente."));
        }
    }
}
