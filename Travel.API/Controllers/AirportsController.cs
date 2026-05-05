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
    public class AirportsController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportsController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<AirportResponseDto>.Ok(await _airportService.GetByIdAsync(id, ct)));

        [HttpGet("by-destination/{destinationId:long}")]
        public async Task<IActionResult> GetByDestination(long destinationId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _airportService.GetByDestinationAsync(destinationId, ct)));

        [HttpGet("iata/{code}")]
        public async Task<IActionResult> GetByIata(string code, CancellationToken ct)
            => Ok(ApiResponse<AirportResponseDto>.Ok(await _airportService.GetByIataCodeAsync(code, ct)));

        [HttpPost]
        [Authorize(Roles = "Administrador" /*"Admin"*/)]
        public async Task<IActionResult> Create([FromBody] CreateAirportRequest request, CancellationToken ct)
        {
            var result = await _airportService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<AirportResponseDto>.Created(result));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _airportService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Aeropuerto eliminado."));
        }
    }
}
