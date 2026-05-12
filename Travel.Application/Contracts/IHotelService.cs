using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IHotelService
    {
        Task<HotelResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<HotelResponseDto>> GetByDestinationAsync(long destinationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<HotelResponseDto>> GetByStarsAsync(int stars, CancellationToken cancellationToken = default);
        Task<IEnumerable<HotelResponseDto>> GetAvailableAsync(long destinationId, System.DateTime checkIn, System.DateTime checkOut, CancellationToken cancellationToken = default);
        Task<HotelResponseDto> CreateAsync(CreateHotelRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);

        // Rooms
        Task<IEnumerable<RoomResponseDto>> GetRoomsByHotelAsync(long hotelId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RoomResponseDto>> GetAvailableRoomsAsync(long hotelId, System.DateTime checkIn, System.DateTime checkOut, CancellationToken cancellationToken = default);
        Task<RoomResponseDto> AddRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default);
        Task DeleteRoomAsync(long roomId, CancellationToken cancellationToken = default);
    }
}
