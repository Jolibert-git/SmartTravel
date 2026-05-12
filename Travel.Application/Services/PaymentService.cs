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
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<PaymentResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Payments.GetWithDetails().FirstOrDefaultAsync(p => p.Id == id, ct)
                ?? throw new NotFoundException("Pago", id);
            return _mapper.Map<PaymentResponseDto>(entity);
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetByReservationAsync(long reservationId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PaymentResponseDto>>(
                await _uow.Payments.GetByReservation(reservationId).ToListAsync(ct));

        public async Task<decimal> GetTotalPaidAsync(long reservationId, CancellationToken ct = default)
            => await _uow.Payments.GetTotalPaidAsync(reservationId, ct);

        public async Task<IEnumerable<PaymentResponseDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PaymentResponseDto>>(
                await _uow.Payments.GetByDateRange(from, to).ToListAsync(ct));

        public async Task<PaymentResponseDto> CreateAsync(CreatePaymentRequest request, CancellationToken ct = default)
        {
            _ = await _uow.Reservations.GetByIdAsync(request.IdReservation, ct)
                ?? throw new NotFoundException("Reserva", request.IdReservation);

            _ = await _uow.PaymentMethods.GetByIdAsync(request.IdPaymentMethod, ct)
                ?? throw new NotFoundException("Método de pago", request.IdPaymentMethod);

            var pendingStatus = await _uow.PaymentStatuses.GetByDescriptionAsync("Pendiente", ct)
                ?? throw new BusinessRuleException("Estado 'Pendiente' no configurado.", "MISSING_STATUS");

            var payment = new Payment
            {
                IdReservation = request.IdReservation,
                IdPaymentMethod = request.IdPaymentMethod,
                Amount = request.Amount,
                IdPaymentStatus = pendingStatus.Id,
                PaymentDate = DateTime.Now
            };

            await _uow.Payments.AddAsync(payment, ct);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(payment.Id, ct);
        }
    }

}
