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
        public IReservationPromotionRepository ReservationPromotions { get; }
        public IFlightSeatReservationRepository FlightSeatReservations { get; }

        // Pagos
        public IPaymentRepository Payments { get; }

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
                            IReservationPromotionRepository ReservationPromotions,
                            IFlightSeatReservationRepository FlightSeatReservations,
                            IPaymentRepository Payments
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
            this.ReservationPromotions = ReservationPromotions;
            this.FlightSeatReservations = FlightSeatReservations;
            this.Payments = Payments;
        }

        // ----------------------------------------------------------------
        // Propiedades — lazy: se instancian solo la primera vez que se usan
        // ----------------------------------------------------------------

        //// Catálogos
        //public IRolRepository Roles => _roles ??= new RolRepository(_context);
        //public ITypeServiceRepository TypeServices => _typeServices ??= new TypeServiceRepository(_context);
        //public ITypeRoomRepository TypeRooms => _typeRooms ??= new TypeRoomRepository(_context);
        //public IDocumentTypeRepository DocumentTypes => _documentTypes ??= new DocumentTypeRepository(_context);
        //public IAvailabilityStatusRepository AvailabilityStatuses => _availabilityStatuses ??= new AvailabilityStatusRepository(_context);
        //public IReservationStatusRepository ReservationStatuses => _reservationStatuses ??= new ReservationStatusRepository(_context);
        //public IDiscountTypeRepository DiscountTypes => _discountTypes ??= new DiscountTypeRepository(_context);
        //public ISeatClassRepository SeatClasses => _seatClasses ??= new SeatClassRepository(_context);
        //public IPaymentStatusRepository PaymentStatuses => _paymentStatuses ??= new PaymentStatusRepository(_context);
        //public IPaymentMethodRepository PaymentMethods => _paymentMethods ??= new PaymentMethodRepository(_context);

        //// Usuarios
        //public ISystemsUserRepository Users => _users ??= new SystemsUserRepository(_context);

        //// Proveedores y servicios
        //public ISupplierRepository Suppliers => _suppliers ??= new SupplierRepository(_context);
        //public IPhoneSupplierRepository PhoneSuppliers => _phoneSuppliers ??= new PhoneSupplierRepository(_context);
        //public IOfferedServiceRepository OfferedServices => _offeredServices ??= new OfferedServiceRepository(_context);
        //public IServiceAvailabilityRepository ServiceAvailabilities => _serviceAvailabilities ??= new ServiceAvailabilityRepository(_context);

        //// Geografía
        //public ICountryRepository Countries => _countries ??= new CountryRepository(_context);
        //public IDestinationRepository Destinations => _destinations ??= new DestinationRepository(_context);

        //// Hoteles
        //public IHotelRepository Hotels => _hotels ??= new HotelRepository(_context);
        //public IPhoneHotelRepository PhoneHotels => _phoneHotels ??= new PhoneHotelRepository(_context);
        //public IRoomRepository Rooms => _rooms ??= new RoomRepository(_context);

        //// Aeropuertos y vuelos
        //public IAirportRepository Airports => _airports ??= new AirportRepository(_context);
        //public IPhoneAirportRepository PhoneAirports => _phoneAirports ??= new PhoneAirportRepository(_context);
        //public IFlightRepository Flights => _flights ??= new FlightRepository(_context);
        //public IFlightSeatRepository FlightSeats => _flightSeats ??= new FlightSeatRepository(_context);

        //// Vehículos
        //public IVehicleRepository Vehicles => _vehicles ??= new VehicleRepository(_context);

        //// Paquetes
        //public IPackageRepository Packages => _packages ??= new PackageRepository(_context);
        //public IDetailPackageRepository DetailPackages => _detailPackages ??= new DetailPackageRepository(_context);

        //// Promociones
        //public IPromotionRepository Promotions => _promotions ??= new PromotionRepository(_context);
        //public IPromotionDetailRepository PromotionDetails => _promotionDetails ??= new PromotionDetailRepository(_context);

        //// Pasajeros
        //public IPassengerRepository Passengers => _passengers ??= new PassengerRepository(_context);

        //// Reservas
        //public IReservationRepository Reservations => _reservations ??= new ReservationRepository(_context);
        //public IDetailReservationRepository DetailReservations => _detailReservations ??= new DetailReservationRepository(_context);
        //public IReservationPromotionRepository ReservationPromotions => _reservationPromotions ??= new ReservationPromotionRepository(_context);
        //public IFlightSeatReservationRepository FlightSeatReservations => _flightSeatReservations ??= new FlightSeatReservationRepository(_context);

        //// Pagos
        //public IPaymentRepository Payments => _payments ??= new PaymentRepository(_context);

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
