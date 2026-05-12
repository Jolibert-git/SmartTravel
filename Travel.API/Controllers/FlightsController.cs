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
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<FlightResponseDto>.Ok(await _flightService.GetByIdAsync(id, ct)));

        /// <summary>Busca vuelos por origen, destino y fecha.</summary>
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] SearchFlightRequest request, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _flightService.SearchAsync(request, ct)));

        [HttpGet("{id:long}/seats")]
        public async Task<IActionResult> GetSeats(long id, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _flightService.GetSeatsByFlightAsync(id, ct)));

        [HttpGet("{id:long}/seats/available")]
        public async Task<IActionResult> GetAvailableSeatsByClass(
            long id,
            [FromQuery] long seatClassId,
            CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(
                await _flightService.GetAvailableSeatsByClassAsync(id, seatClassId, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateFlightRequest request, CancellationToken ct)
        {
            var result = await _flightService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<FlightResponseDto>.Created(result));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _flightService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Vuelo eliminado."));
        }
    }

}
