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
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<HotelResponseDto>.Ok(await _hotelService.GetByIdAsync(id, ct)));

        [HttpGet("by-destination/{destinationId:long}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDestination(long destinationId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _hotelService.GetByDestinationAsync(destinationId, ct)));

        [HttpGet("by-stars/{stars:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByStars(int stars, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _hotelService.GetByStarsAsync(stars, ct)));

        [HttpGet("available")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailable(
            [FromQuery] long destinationId,
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut,
            CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(
                await _hotelService.GetAvailableAsync(destinationId, checkIn, checkOut, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateHotelRequest request, CancellationToken ct)
        {
            var result = await _hotelService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<HotelResponseDto>.Created(result));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _hotelService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Hotel eliminado."));
        }

        // Rooms
        [HttpGet("{hotelId:long}/rooms")]
        public async Task<IActionResult> GetRooms(long hotelId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _hotelService.GetRoomsByHotelAsync(hotelId, ct)));

        [HttpGet("{hotelId:long}/rooms/available")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailableRooms(
            long hotelId,
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut,
            CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(
                await _hotelService.GetAvailableRoomsAsync(hotelId, checkIn, checkOut, ct)));

        [HttpPost("rooms")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoom([FromBody] CreateRoomRequest request, CancellationToken ct)
            => Ok(ApiResponse<RoomResponseDto>.Created(await _hotelService.AddRoomAsync(request, ct)));

        [HttpDelete("rooms/{roomId:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(long roomId, CancellationToken ct)
        {
            await _hotelService.DeleteRoomAsync(roomId, ct);
            return Ok(ApiResponse<object>.NoContent("Habitación eliminada."));
        }
    }

    // ============================================================
    // VehiclesController
    // ============================================================
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
            => Ok(ApiResponse<VehicleResponseDto>.Ok(await _vehicleService.GetByIdAsync(id, ct)));

        [HttpGet("by-destination/{destinationId:long}")]
        public async Task<IActionResult> GetByDestination(long destinationId, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _vehicleService.GetByDestinationAsync(destinationId, ct)));

        [HttpGet("by-transmission")]
        public async Task<IActionResult> GetByTransmission([FromQuery] string transmission, CancellationToken ct)
            => Ok(ApiResponse<object>.Ok(await _vehicleService.GetByTransmissionAsync(transmission, ct)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request, CancellationToken ct)
        {
            var result = await _vehicleService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<VehicleResponseDto>.Created(result));
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _vehicleService.DeleteAsync(id, ct);
            return Ok(ApiResponse<object>.NoContent("Vehículo eliminado."));
        }
    }
}
