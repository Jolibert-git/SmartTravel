using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Domain.Exceptions;

namespace Travel.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<HotelResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Hotels.GetWithDetails().FirstOrDefaultAsync(h => h.Id == id, ct)
                ?? throw new NotFoundException("Hotel", id);
            return _mapper.Map<HotelResponseDto>(entity);
        }

        public async Task<IEnumerable<HotelResponseDto>> GetByDestinationAsync(long destinationId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<HotelResponseDto>>(
                await _uow.Hotels.GetByDestination(destinationId).ToListAsync(ct));

        public async Task<IEnumerable<HotelResponseDto>> GetByStarsAsync(int stars, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<HotelResponseDto>>(
                await _uow.Hotels.GetByStars(stars).ToListAsync(ct));

        public async Task<IEnumerable<HotelResponseDto>> GetAvailableAsync(long destinationId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<HotelResponseDto>>(
                await _uow.Hotels.GetAvailable(destinationId, checkIn, checkOut).ToListAsync(ct));

        public async Task<HotelResponseDto> CreateAsync(CreateHotelRequest request, CancellationToken ct = default)
        {
            _ = await _uow.Destinations.GetByIdAsync(request.IdDestination, ct)
                ?? throw new NotFoundException("Destino", request.IdDestination);

            var hotel = new Hotel
            {
                IdDestination = request.IdDestination,
                HotelName = request.HotelName,
                Stars = request.Stars,
                Email = request.Email
            };

            await _uow.Hotels.AddAsync(hotel, ct);
            await _uow.SaveChangesAsync(ct);

            foreach (var phone in request.PhoneNumbers)
                await _uow.PhoneHotels.AddAsync(new PhoneHotel { IdHotel = hotel.Id, PhoneNumber = phone }, ct);

            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(hotel.Id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Hotels.GetByIdAsync(id, ct) ?? throw new NotFoundException("Hotel", id);
            _uow.Hotels.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<RoomResponseDto>> GetRoomsByHotelAsync(long hotelId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<RoomResponseDto>>(
                await _uow.Rooms.GetByHotel(hotelId).ToListAsync(ct));

        public async Task<IEnumerable<RoomResponseDto>> GetAvailableRoomsAsync(long hotelId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<RoomResponseDto>>(
                await _uow.Rooms.GetAvailable(hotelId, checkIn, checkOut).ToListAsync(ct));

        public async Task<RoomResponseDto> AddRoomAsync(CreateRoomRequest request, CancellationToken ct = default)
        {
            _ = await _uow.Hotels.GetByIdAsync(request.IdHotel, ct) ?? throw new NotFoundException("Hotel", request.IdHotel);
            _ = await _uow.TypeRooms.GetByIdAsync(request.IdTypeRoom, ct) ?? throw new NotFoundException("Tipo de habitación", request.IdTypeRoom);
            _ = await _uow.OfferedServices.GetByIdAsync(request.IdService, ct) ?? throw new NotFoundException("Servicio", request.IdService);

            var room = new Room { IdHotel = request.IdHotel, IdTypeRoom = request.IdTypeRoom, IdService = request.IdService };
            await _uow.Rooms.AddAsync(room, ct);
            await _uow.SaveChangesAsync(ct);

            var created = await _uow.Rooms.GetWithDetails().FirstOrDefaultAsync(r => r.Id == room.Id, ct);
            return _mapper.Map<RoomResponseDto>(created);
        }

        public async Task DeleteRoomAsync(long roomId, CancellationToken ct = default)
        {
            var room = await _uow.Rooms.GetByIdAsync(roomId, ct) ?? throw new NotFoundException("Habitación", roomId);
            _uow.Rooms.Delete(room);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
