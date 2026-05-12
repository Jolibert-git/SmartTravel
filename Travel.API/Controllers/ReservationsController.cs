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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<ReservationResponseDto>.Ok(await _reservationService.GetByIdAsync(id, ct)));

        [HttpGet("my")]
        public async Task<IActionResult> GetMyReservations(CancellationToken ct)
        {
            var userId = GetCurrentUserId();
            return Ok(ApiResponse<object>.Ok(await _reservationService.GetByUserAsync(userId, ct)));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
            => Ok(await _reservationService.GetPagedAsync(page, pageSize, ct));

        [HttpGet("by-status/{statusId:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByStatus(long statusId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _reservationService.GetByStatusAsync(statusId, ct)));

        [HttpGet("by-date-range")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByDateRange(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _reservationService.GetByDateRangeAsync(from, to, ct)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request, CancellationToken ct)
        {
            var userId = GetCurrentUserId();
            var result = await _reservationService.CreateAsync(userId, request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<ReservationResponseDto>.Created(result, "Reserva creada exitosamente."));
        }

        [HttpPatch("{id:long}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(
            long id,
            [FromBody] UpdateReservationStatusRequest request,
            CancellationToken ct)
            => Ok(ApiResponse<ReservationResponseDto>.Ok(
                await _reservationService.UpdateStatusAsync(id, request, ct),
                "Estado actualizado correctamente."));

        [HttpPatch("{id:long}/cancel")]
        public async Task<IActionResult> Cancel(long id, CancellationToken ct)
        {
            await _reservationService.CancelAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Reserva cancelada."));
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDashboard(CancellationToken ct)
            => Ok(ApiResponse<DashboardSummaryDto>.Ok(
                await _reservationService.GetDashboardSummaryAsync(ct)));

        private long GetCurrentUserId()
        {
            var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return long.TryParse(claim, out var id) ? id : 0;
        }
    }
}
