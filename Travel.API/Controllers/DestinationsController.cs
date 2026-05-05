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
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _destinationService;

        public DestinationsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _destinationService.GetAllAsync(ct)));

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<DestinationResponseDto>.Ok(await _destinationService.GetByIdAsync(id, ct)));

        [HttpGet("by-country/{countryId:long}")]
        public async Task<IActionResult> GetByCountry(long countryId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _destinationService.GetByCountryAsync(countryId, ct)));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string city, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _destinationService.SearchByCityAsync(city, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDestinationRequest request, CancellationToken ct)
        {
            var result = await _destinationService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<DestinationResponseDto>.Created(result));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _destinationService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Destino eliminado."));
        }
    }
}

