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
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<PromotionResponseDto>.Ok(await _promotionService.GetByIdAsync(id, ct)));

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActive(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _promotionService.GetActiveAsync(ct)));

        [HttpGet("by-service/{serviceId:long}")]
        public async Task<IActionResult> GetByService(long serviceId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _promotionService.GetByServiceAsync(serviceId, ct)));

        [HttpGet("by-package/{packageId:long}")]
        public async Task<IActionResult> GetByPackage(long packageId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _promotionService.GetByPackageAsync(packageId, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePromotionRequest request, CancellationToken ct)
        {
            var result = await _promotionService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<PromotionResponseDto>.Created(result));
        }

        [HttpPatch("{id:long}/toggle")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Toggle(long id, CancellationToken ct)
        {
            await _promotionService.ToggleActiveAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Estado de la promoción actualizado."));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _promotionService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Promoción eliminada."));
        }
    }
}
