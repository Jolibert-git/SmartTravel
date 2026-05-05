using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Contracts;
using Travel.Application.Request;
using Travel.Application.Responses;

namespace Travel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogsController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        // TypeService
        [HttpGet("type-services")]
        public async Task<IActionResult> GetTypeServices(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetAllTypeServicesAsync(ct)));

        [HttpPost("type-services")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTypeService([FromBody] CreateTypeServiceRequest request, CancellationToken ct)
            => Ok(ApiResponse<object>.Created(await _catalogService.CreateTypeServiceAsync(request, ct)));

        [HttpDelete("type-services/{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTypeService(long id, CancellationToken ct)
        {
            await _catalogService.DeleteTypeServiceAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent());
        }

        // TypeRoom
        [HttpGet("type-rooms")]
        public async Task<IActionResult> GetTypeRooms(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetAllTypeRoomsAsync(ct)));

        [HttpPost("type-rooms")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTypeRoom([FromBody] CreateTypeRoomRequest request, CancellationToken ct)
            => Ok(ApiResponse<object>.Created(await _catalogService.CreateTypeRoomAsync(request, ct)));

        [HttpDelete("type-rooms/{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTypeRoom(long id, CancellationToken ct)
        {
            await _catalogService.DeleteTypeRoomAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent());
        }

        // Countries
        [HttpGet("countries")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountries(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetAllCountriesAsync(ct)));

        [HttpGet("countries/{id:long}")]
        public async Task<IActionResult> GetCountryById(long id, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetCountryByIdAsync(id, ct)));

        [HttpPost("countries")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequest request, CancellationToken ct)
            => Ok(ApiResponse<object>.Created(await _catalogService.CreateCountryAsync(request, ct)));

        // Seat Classes
        [HttpGet("seat-classes")]
        public async Task<IActionResult> GetSeatClasses(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetAllSeatClassesAsync(ct)));

        // Document Types
        [HttpGet("document-types")]
        public async Task<IActionResult> GetDocumentTypes(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetAllDocumentTypesAsync(ct)));

        // Discount Types
        [HttpGet("discount-types")]
        public async Task<IActionResult> GetDiscountTypes(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetAllDiscountTypesAsync(ct)));

        // Payment Methods
        [HttpGet("payment-methods")]
        public async Task<IActionResult> GetPaymentMethods(CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _catalogService.GetActivePaymentMethodsAsync(ct)));
    }

}
