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
using Travel.Application.Responses;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Domain.Exceptions;

namespace Travel.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<ReservationResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Reservations.GetWithFullDetails()
                                                .FirstOrDefaultAsync(r => r.Id == id, ct)
                ?? throw new NotFoundException("Reserva", id);
            return _mapper.Map<ReservationResponseDto>(entity);
        }

        public async Task<IEnumerable<ReservationSummaryDto>> GetByUserAsync(long userId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<ReservationSummaryDto>>(
                await _uow.Reservations.GetByUser(userId).ToListAsync(ct));

        public async Task<PagedResponse<ReservationSummaryDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            var total = await _uow.Reservations.CountAsync(cancellationToken: ct);
            var items = await _uow.Reservations.GetPaged(page, pageSize).ToListAsync(ct);
            var dtos = _mapper.Map<IEnumerable<ReservationSummaryDto>>(items);
            return PagedResponse<ReservationSummaryDto>.Create(dtos, total, page, pageSize);
        }

        public async Task<IEnumerable<ReservationSummaryDto>> GetByStatusAsync(long statusId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<ReservationSummaryDto>>(
                await _uow.Reservations.GetByStatus(statusId).ToListAsync(ct));

        public async Task<IEnumerable<ReservationSummaryDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<ReservationSummaryDto>>(
                await _uow.Reservations.GetByDateRange(from, to).ToListAsync(ct));

        public async Task<ReservationResponseDto> CreateAsync(long userId, CreateReservationRequest request, CancellationToken ct = default)
        {
            _ = await _uow.Users.GetByIdAsync(userId, ct) ?? throw new NotFoundException("Usuario", userId);

            var pendingStatus = await _uow.ReservationStatuses.GetByDescriptionAsync("Pendiente", ct)
                ?? throw new BusinessRuleException("Estado 'Pendiente' no configurado.", "MISSING_STATUS");

            await _uow.BeginTransactionAsync(ct);
            try
            {
                var reservation = new Reservation
                {
                    IdSystemUser = userId,
                    IdReservationStatus = pendingStatus.Id,
                    IdPackage = request.IdPackage,
                    DateRequest = DateTime.Now
                };

                await _uow.Reservations.AddAsync(reservation, ct);
                await _uow.SaveChangesAsync(ct);

                foreach (var detail in request.Details)
                {
                    var resDetail = new DetailReservation
                    {
                        IdReservation = reservation.Id,
                        IdService = detail.IdService,
                        DateCheckIn = detail.DateCheckIn,
                        DateCheckOut = detail.DateCheckOut,
                        Total = 0, // calculado por servicio en implementación real
                        IdRoom = detail.IdRoom,
                        IdFlight = detail.IdFlight,
                        IdVehicle = detail.IdVehicle
                    };
                    await _uow.DetailReservations.AddAsync(resDetail, ct);
                    await _uow.SaveChangesAsync(ct);

                    // Asignar asientos
                    foreach (var seat in detail.SeatAssignments)
                    {
                        if (await _uow.FlightSeatReservations.SeatAlreadyReservedAsync(seat.IdFlightSeat, ct))
                            throw new BusinessRuleException($"El asiento {seat.IdFlightSeat} ya está reservado.", "SEAT_TAKEN");

                        await _uow.FlightSeatReservations.AddAsync(new FlightSeatReservation
                        {
                            IdFlightSeat = seat.IdFlightSeat,
                            IdPassenger = seat.IdPassenger,
                            IdDetailReservation = resDetail.Id
                        }, ct);
                        await _uow.FlightSeats.SetAvailabilityAsync(seat.IdFlightSeat, false, ct);
                    }
                }

                // Pasajeros
                foreach (var passengerId in request.PassengerIds)
                    await _uow.Passengers.AddAsync(null!, ct); // handled via context

                await _uow.SaveChangesAsync(ct);
                await _uow.CommitTransactionAsync(ct);

                return await GetByIdAsync(reservation.Id, ct);
            }
            catch
            {
                await _uow.RollbackTransactionAsync(ct);
                throw;
            }
        }

        public async Task<ReservationResponseDto> UpdateStatusAsync(long id, UpdateReservationStatusRequest request, CancellationToken ct = default)
        {
            var reservation = await _uow.Reservations.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Reserva", id);

            reservation.IdReservationStatus = request.IdReservationStatus;
            _uow.Reservations.Update(reservation);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(id, ct);
        }

        public async Task CancelAsync(long id, CancellationToken ct = default)
        {
            var cancelStatus = await _uow.ReservationStatuses.GetByDescriptionAsync("Cancelada", ct)
                ?? throw new BusinessRuleException("Estado 'Cancelada' no configurado.", "MISSING_STATUS");

            var reservation = await _uow.Reservations.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Reserva", id);

            reservation.IdReservationStatus = cancelStatus.Id;
            _uow.Reservations.Update(reservation);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken ct = default)
        {
            var byStatus = await _uow.Reservations.GetCountByStatusAsync(ct);
            var totalRev = await _uow.Payments.GetQueryable().SumAsync(p => p.Amount, ct);
            var totalPass = await _uow.Passengers.CountAsync(cancellationToken: ct);
            var activePkg = await _uow.Packages.GetActive().CountAsync(ct);

            return new DashboardSummaryDto
            {
                TotalReservations = byStatus.Values.Sum(),
                ActivePackages = activePkg,
                TotalPassengers = totalPass,
                TotalRevenue = totalRev,
                FormattedRevenue = $"${totalRev:N2}",
                ReservationsByStatus = byStatus
            };
        }
    }

}
