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
    public class PassengersController : ControllerBase
    {
        private readonly IPassengerService _passengerService;

        public PassengersController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<PassengerResponseDto>.Ok(await _passengerService.GetByIdAsync(id, ct)));

        [HttpGet("document/{documentNumber}")]
        public async Task<IActionResult> GetByDocument(string documentNumber, CancellationToken ct)
            => Ok(ApiResponse<PassengerResponseDto>.Ok(
                await _passengerService.GetByDocumentAsync(documentNumber, ct)));

        [HttpGet("by-reservation/{reservationId:long}")]
        public async Task<IActionResult> GetByReservation(long reservationId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(
                await _passengerService.GetByReservationAsync(reservationId, ct)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePassengerRequest request, CancellationToken ct)
        {
            var result = await _passengerService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<PassengerResponseDto>.Created(result));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _passengerService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Pasajero eliminado."));
        }
    }

}
