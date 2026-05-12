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
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<PackageResponseDto>.Ok(await _packageService.GetByIdAsync(id, ct)));

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActive(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _packageService.GetActiveAsync(ct)));

        [HttpGet("expiring")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetExpiring([FromQuery] int days = 7, CancellationToken ct = default)
            => Ok(ApiResponse<object>.Ok(await _packageService.GetExpiringAsync(days, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePackageRequest request, CancellationToken ct)
        {
            var result = await _packageService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<PackageResponseDto>.Created(result));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _packageService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Paquete eliminado."));
        }
    }
}
