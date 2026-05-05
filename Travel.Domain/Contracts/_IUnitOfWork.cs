using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        // ----------------------------------------------------------------
        // Catálogos simples
        // ----------------------------------------------------------------
        IRolRepository Roles { get; }
        ITypeServiceRepository TypeServices { get; }
        ITypeRoomRepository TypeRooms { get; }
        IDocumentTypeRepository DocumentTypes { get; }
        IAvailabilityStatusRepository AvailabilityStatuses { get; }
        IReservationStatusRepository ReservationStatuses { get; }
        IDiscountTypeRepository DiscountTypes { get; }
        ISeatClassRepository SeatClasses { get; }
        IPaymentStatusRepository PaymentStatuses { get; }
        IPaymentMethodRepository PaymentMethods { get; }

        //// ----------------------------------------------------------------
        //// Usuarios y seguridad
        //// ----------------------------------------------------------------
        ISystemsUserRepository Users { get; }

        //// ----------------------------------------------------------------
        //// Proveedores y servicios
        //// ----------------------------------------------------------------
        ISupplierRepository Suppliers { get; }
        IPhoneSupplierRepository PhoneSuppliers { get; }
        IOfferedServiceRepository OfferedServices { get; }
        IServiceAvailabilityRepository ServiceAvailabilities { get; }

        //// ----------------------------------------------------------------
        //// Geografía
        //// ----------------------------------------------------------------
        ICountryRepository Countries { get; }
        IDestinationRepository Destinations { get; }

        //// ----------------------------------------------------------------
        //// Hoteles
        //// ----------------------------------------------------------------
        IHotelRepository Hotels { get; }
        IPhoneHotelRepository PhoneHotels { get; }
        IRoomRepository Rooms { get; }

        //// ----------------------------------------------------------------
        //// Aeropuertos y vuelos
        //// ----------------------------------------------------------------
        IAirportRepository Airports { get; }
        IPhoneAirportRepository PhoneAirports { get; }
        IFlightRepository Flights { get; }
        IFlightSeatRepository FlightSeats { get; }

        //// ----------------------------------------------------------------
        //// Vehículos
        //// ----------------------------------------------------------------
        IVehicleRepository Vehicles { get; }

        //// ----------------------------------------------------------------
        //// Paquetes
        //// ----------------------------------------------------------------
        IPackageRepository Packages { get; }
        IDetailPackageRepository DetailPackages { get; }

        //// ----------------------------------------------------------------
        //// Promociones
        //// ----------------------------------------------------------------
        IPromotionRepository Promotions { get; }
        IPromotionDetailRepository PromotionDetails { get; }

        //// ----------------------------------------------------------------
        //// Pasajeros
        //// ----------------------------------------------------------------
        IPassengerRepository Passengers { get; }

        //// ----------------------------------------------------------------
        //// Reservas
        //// ----------------------------------------------------------------
        IReservationRepository Reservations { get; }
        IDetailReservationRepository DetailReservations { get; }
        IReservationPromotionRepository ReservationPromotions { get; }
        IFlightSeatReservationRepository FlightSeatReservations { get; }

        //// ----------------------------------------------------------------
        //// Pagos
        //// ----------------------------------------------------------------
        IPaymentRepository Payments { get; }

        // ----------------------------------------------------------------
        // Persistencia y transacciones
        // ----------------------------------------------------------------

        /// <summary>Persiste todos los cambios pendientes en la BD.</summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>Inicia una transacción explícita (operaciones multi-tabla).</summary>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>Confirma la transacción activa.</summary>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>Revierte la transacción activa en caso de error.</summary>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
