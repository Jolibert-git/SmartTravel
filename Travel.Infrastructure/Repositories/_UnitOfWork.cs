using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Contracts;
using Travel.Persistence.Context;

namespace Travel.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        // ----------------------------------------------------------------
        // Backing fields — lazy init con ??=
        // ----------------------------------------------------------------

        // Catálogos
        public IRolRepository Roles { get; }
        public ITypeServiceRepository TypeServices { get; }
        public ITypeRoomRepository TypeRooms { get; }
        public IDocumentTypeRepository DocumentTypes { get; }
        public IAvailabilityStatusRepository AvailabilityStatuses { get; }
        public IReservationStatusRepository ReservationStatuses { get; }
        public IDiscountTypeRepository DiscountTypes { get; }
        public ISeatClassRepository SeatClasses { get; }
        public IPaymentStatusRepository PaymentStatuses { get; }
        public IPaymentMethodRepository PaymentMethods { get; }

        // Usuarios
        public ISystemsUserRepository Users { get; }

        // Proveedores y servicios
        public ISupplierRepository Suppliers { get; }
        public IPhoneSupplierRepository PhoneSuppliers { get; }
        public IOfferedServiceRepository OfferedServices { get; }
        public IServiceAvailabilityRepository ServiceAvailabilities { get; }

        // Geografía
        public ICountryRepository Countries { get; }
        public IDestinationRepository Destinations { get; }

        // Hoteles
        public IHotelRepository Hotels { get; }
        public IPhoneHotelRepository PhoneHotels { get; }
        public IRoomRepository Rooms { get; }

        // Aeropuertos y vuelos
        public IAirportRepository Airports { get; }
        public IPhoneAirportRepository PhoneAirports { get; }
        public IFlightRepository Flights { get; }
        public IFlightSeatRepository FlightSeats { get; }

        // Vehículos
        public IVehicleRepository Vehicles { get; }

        // Paquetes
        public IPackageRepository Packages { get; }
        public IDetailPackageRepository DetailPackages { get; }

        // Promociones
        public IPromotionRepository Promotions { get; }
        public IPromotionDetailRepository PromotionDetails { get; }

        // Pasajeros
        public IPassengerRepository Passengers { get; }

        // Reservas
        public IReservationRepository Reservations { get; }
        public IDetailReservationRepository DetailReservations { get; }
        public IReservationPassengerRepository ReservationPassengers { get; }
        public IReservationPromotionRepository ReservationPromotions { get; }
        public IFlightSeatReservationRepository FlightSeatReservations { get; }

        // Pagos
        public IPaymentRepository Payments { get; }

        public IPayPalPaymentRepository PayPalPayments { get; }

        public UnitOfWork(  ApplicationDbContext context,
                            //IDbContextTransaction? _transaction,
                            IRolRepository Roles,
                            ITypeServiceRepository TypeServices,
                            ITypeRoomRepository TypeRooms,
                            IDocumentTypeRepository DocumentTypes,
                            IAvailabilityStatusRepository AvailabilityStatuses,
                            IReservationStatusRepository ReservationStatuses,
                            IDiscountTypeRepository DiscountTypes,
                            ISeatClassRepository SeatClasses,
                            IPaymentStatusRepository PaymentStatuses,
                            IPaymentMethodRepository PaymentMethods,
                            ISystemsUserRepository Users,
                            ISupplierRepository Suppliers,
                            IPhoneSupplierRepository PhoneSuppliers,
                            IOfferedServiceRepository OfferedServices,
                            IServiceAvailabilityRepository ServiceAvailabilities,
                            ICountryRepository Countries,
                            IDestinationRepository Destinations,
                            IHotelRepository Hotels,
                            IPhoneHotelRepository PhoneHotels,
                            IRoomRepository Rooms,
                            IAirportRepository Airports,
                            IPhoneAirportRepository PhoneAirports,
                            IFlightRepository Flights,
                            IFlightSeatRepository FlightSeats,
                            IVehicleRepository Vehicles,
                            IPackageRepository Packages,
                            IDetailPackageRepository DetailPackages,
                            IPromotionRepository Promotions,
                            IPromotionDetailRepository PromotionDetails,
                            IPassengerRepository Passengers,
                            IReservationRepository Reservations,
                            IDetailReservationRepository DetailReservations,
                            IReservationPassengerRepository ReservationPassengers,
                            IReservationPromotionRepository ReservationPromotions,
                            IFlightSeatReservationRepository FlightSeatReservations,
                            IPaymentRepository Payments,
                            IPayPalPaymentRepository PayPalPayments
                      )
        {
            _context = context;
            //this._transaction = _transaction;
            this.Roles = Roles;
            this.TypeServices = TypeServices;
            this.TypeRooms = TypeRooms;
            this.DocumentTypes = DocumentTypes;
            this.AvailabilityStatuses = AvailabilityStatuses;
            this.ReservationStatuses = ReservationStatuses;
            this.DiscountTypes = DiscountTypes;
            this.SeatClasses = SeatClasses;
            this.PaymentStatuses = PaymentStatuses;
            this.PaymentMethods = PaymentMethods;
            this.Users = Users;
            this.Suppliers = Suppliers;
            this.PhoneSuppliers = PhoneSuppliers;
            this.OfferedServices = OfferedServices;
            this.ServiceAvailabilities = ServiceAvailabilities;
            this.Countries = Countries;
            this.Destinations = Destinations;
            this.Hotels = Hotels;
            this.PhoneHotels = PhoneHotels;
            this.Rooms = Rooms;
            this.Airports = Airports;
            this.PhoneAirports = PhoneAirports;
            this.Flights = Flights;
            this.FlightSeats = FlightSeats;
            this.Vehicles = Vehicles;
            this.Packages = Packages;
            this.DetailPackages = DetailPackages;
            this.Promotions = Promotions;
            this.PromotionDetails = PromotionDetails;
            this.Passengers = Passengers;
            this.Reservations = Reservations;
            this.DetailReservations = DetailReservations;
            this.ReservationPassengers = ReservationPassengers;
            this.ReservationPromotions = ReservationPromotions;
            this.FlightSeatReservations = FlightSeatReservations;
            this.Payments = Payments;
            //this.PayPalPayments = PayPalPayments;
        }

        

        // ----------------------------------------------------------------
        // Persistencia y transacciones
        // ----------------------------------------------------------------

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
            => _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null) return;
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null) return;
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
