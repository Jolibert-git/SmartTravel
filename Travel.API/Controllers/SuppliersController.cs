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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _supplierService.GetAllAsync(ct)));

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<SupplierResponseDto>.Ok(await _supplierService.GetByIdAsync(id, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request, CancellationToken ct)
        {
            var result = await _supplierService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<SupplierResponseDto>.Created(result));
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateSupplierRequest request, CancellationToken ct)
            => Ok(ApiResponse<SupplierResponseDto>.Ok(
                await _supplierService.UpdateAsync(id, request, ct), "Proveedor actualizado."));

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _supplierService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Proveedor eliminado."));
        }
    }

}
