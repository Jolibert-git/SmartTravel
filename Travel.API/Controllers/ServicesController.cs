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
    public class ServicesController : ControllerBase
    {
        private readonly IServiceManagementService _service;

        public ServicesController(IServiceManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _service.GetAllAsync(ct)));

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<ServiceResponseDto>.Ok(await _service.GetByIdAsync(id, ct)));

        [HttpGet("by-type/{typeId:long}")]
        public async Task<IActionResult> GetByType(long typeId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _service.GetByTypeAsync(typeId, ct)));

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable([FromQuery] DateTime date, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _service.GetAvailableAsync(date, ct)));

        [HttpGet("price-range")]
        public async Task<IActionResult> GetByPriceRange(
            [FromQuery] decimal min,
            [FromQuery] decimal max,
            CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _service.GetByPriceRangeAsync(min, max, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequest request, CancellationToken ct)
        {
            var result = await _service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<ServiceResponseDto>.Created(result));
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateServiceRequest request, CancellationToken ct)
            => Ok(ApiResponse<ServiceResponseDto>.Ok(await _service.UpdateAsync(id, request, ct)));

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Servicio eliminado."));
        }
    }
}
