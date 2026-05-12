using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Persistence.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // ================================================================
        // DbSets — una por entidad
        // ================================================================

        // Seguridad
        public DbSet<Rol> Roles { get; set; }
        public DbSet<SystemsUser> SystemsUsers { get; set; }
        public DbSet<RolUser> RolUsers { get; set; }

        // Catálogos
        public DbSet<TypeService> TypeServices { get; set; }
        public DbSet<TypeRoom> TypeRooms { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<SeatClass> SeatClasses { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        // Proveedores y servicios
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PhoneSupplier> PhoneSuppliers { get; set; }
        public DbSet<OfferedService> OfferedServices { get; set; }
        public DbSet<ServiceAvailability> ServiceAvailabilities { get; set; }

        // Geografía
        public DbSet<Country> Countries { get; set; }
        public DbSet<Destination> Destinations { get; set; }

        // Hoteles
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<PhoneHotel> PhoneHotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        // Aeropuertos y vuelos
        public DbSet<Airport> Airports { get; set; }
        public DbSet<PhoneAirport> PhoneAirports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightSeat> FlightSeats { get; set; }

        // Vehículos
        public DbSet<Vehicle> Vehicles { get; set; }

        // Paquetes
        public DbSet<Package> Packages { get; set; }
        public DbSet<DetailPackage> DetailPackages { get; set; }

        // Promociones
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionDetail> PromotionDetails { get; set; }

        // Pasajeros
        public DbSet<Passenger> Passengers { get; set; }

        // Reservas
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<DetailReservation> DetailReservations { get; set; }
        public DbSet<ReservationPassenger> ReservationPassengers { get; set; }
        public DbSet<ReservationPromotion> ReservationPromotions { get; set; }
        public DbSet<FlightSeatReservation> FlightSeatReservations { get; set; }

        // Pagos
        public DbSet<Payment> Payments { get; set; }

        public DbSet<PayPalPayment> PayPalPayments { get; set; }


        // ================================================================
        // Fluent API — configuraciones que Data Annotations no cubre
        // ================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ----------------------------------------------------------------
            // RolUser — clave primaria compuesta
            // ----------------------------------------------------------------
            modelBuilder.Entity<RolUser>(entity =>
            {
                entity.HasKey(ru => new { ru.IdRol, ru.IdSystemUser });

                entity.HasOne(ru => ru.Rol)
                      .WithMany(r => r.RolUsers)
                      .HasForeignKey(ru => ru.IdRol)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ru => ru.SystemsUser)
                      .WithMany(u => u.RolUsers)
                      .HasForeignKey(ru => ru.IdSystemUser)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ----------------------------------------------------------------
            // ReservationPassenger — clave primaria compuesta
            // ----------------------------------------------------------------
            modelBuilder.Entity<ReservationPassenger>(entity =>
            {
                entity.HasKey(rp => new { rp.IdReservation, rp.IdPassenger });

                entity.HasOne(rp => rp.Reservation)
                      .WithMany(r => r.ReservationPassengers)
                      .HasForeignKey(rp => rp.IdReservation)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Passenger)
                      .WithMany(p => p.ReservationPassengers)
                      .HasForeignKey(rp => rp.IdPassenger)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ----------------------------------------------------------------
            // Flight — dos FKs al mismo Airport (evita múltiples cascade paths)
            // ----------------------------------------------------------------
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasOne(f => f.AirportOrigen)
                      .WithMany(a => a.DepartureFlights)
                      .HasForeignKey(f => f.AirportIdOrigen)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.AirportArrive)
                      .WithMany(a => a.ArrivalFlights)
                      .HasForeignKey(f => f.AirportIdArrive)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ----------------------------------------------------------------
            // DetailReservation — múltiples FKs opcionales (Room, Flight, Vehicle)
            // ----------------------------------------------------------------
            modelBuilder.Entity<DetailReservation>(entity =>
            {
                entity.HasOne(dr => dr.Room)
                      .WithMany(r => r.DetailReservations)
                      .HasForeignKey(dr => dr.IdRoom)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(dr => dr.Flight)
                      .WithMany(f => f.DetailReservations)
                      .HasForeignKey(dr => dr.IdFlight)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(dr => dr.Vehicle)
                      .WithMany(v => v.DetailReservations)
                      .HasForeignKey(dr => dr.IdVehicle)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(dr => dr.OfferedService)
                      .WithMany(s => s.DetailReservations)
                      .HasForeignKey(dr => dr.IdService)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(dr => dr.Reservation)
                      .WithMany(r => r.DetailReservations)
                      .HasForeignKey(dr => dr.IdReservation)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ----------------------------------------------------------------
            // FlightSeatReservation — evitar múltiples cascade paths
            // ----------------------------------------------------------------
            modelBuilder.Entity<FlightSeatReservation>(entity =>
            {
                entity.HasOne(fsr => fsr.FlightSeat)
                      .WithMany(fs => fs.FlightSeatReservations)
                      .HasForeignKey(fsr => fsr.IdFlightSeat)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(fsr => fsr.Passenger)
                      .WithMany(p => p.FlightSeatReservations)
                      .HasForeignKey(fsr => fsr.IdPassenger)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(fsr => fsr.DetailReservation)
                      .WithMany(dr => dr.FlightSeatReservations)
                      .HasForeignKey(fsr => fsr.IdDetailReservation)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ----------------------------------------------------------------
            // PromotionDetail — dos FKs opcionales (Service o Package)
            // ----------------------------------------------------------------
            modelBuilder.Entity<PromotionDetail>(entity =>
            {
                entity.HasOne(pd => pd.OfferedService)
                      .WithMany(s => s.PromotionDetails)
                      .HasForeignKey(pd => pd.IdService)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pd => pd.Package)
                      .WithMany(p => p.PromotionDetails)
                      .HasForeignKey(pd => pd.IdPackage)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ----------------------------------------------------------------
            // ReservationPromotion — evitar cascade sobre Reservation
            // ----------------------------------------------------------------
            modelBuilder.Entity<ReservationPromotion>(entity =>
            {
                entity.HasOne(rp => rp.Reservation)
                      .WithMany(r => r.ReservationPromotions)
                      .HasForeignKey(rp => rp.IdReservation)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Promotion)
                      .WithMany(p => p.ReservationPromotions)
                      .HasForeignKey(rp => rp.IdPromotion)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ----------------------------------------------------------------
            // Reservation — FK a Package opcional sin cascade
            // ----------------------------------------------------------------
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(r => r.Package)
                      .WithMany(p => p.Reservations)
                      .HasForeignKey(r => r.IdPackage)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.SystemsUser)
                      .WithMany(u => u.Reservations)
                      .HasForeignKey(r => r.IdSystemUser)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ----------------------------------------------------------------
            // Supplier — unique index en RNC
            // ----------------------------------------------------------------
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasIndex(s => s.Rnc).IsUnique();
            });

            // ----------------------------------------------------------------
            // SystemsUser — unique index en Email
            // ----------------------------------------------------------------
            modelBuilder.Entity<SystemsUser>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
            });

            // ----------------------------------------------------------------
            // Country — unique index en IsoCode
            // ----------------------------------------------------------------
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(c => c.IsoCode).IsUnique();
            });

            // ----------------------------------------------------------------
            // Airport — unique index en CodeIata
            // ----------------------------------------------------------------
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasIndex(a => a.CodeIata).IsUnique();
            });

            // ----------------------------------------------------------------
            // Ignorar propiedades [NotMapped] — EF las ignora automáticamente
            // pero se declara explícitamente para mayor claridad
            // ----------------------------------------------------------------
            modelBuilder.Entity<SystemsUser>().Ignore(u => u.FullName);
            modelBuilder.Entity<SystemsUser>().Ignore(u => u.Initials);
            modelBuilder.Entity<Rol>().Ignore(r => r.DisplayName);
            modelBuilder.Entity<OfferedService>().Ignore(s => s.FormattedPrice);
            modelBuilder.Entity<OfferedService>().Ignore(s => s.ServiceTypeLabel);
            modelBuilder.Entity<ServiceAvailability>().Ignore(sa => sa.DateRangeDisplay);
            modelBuilder.Entity<ServiceAvailability>().Ignore(sa => sa.IsCurrentlyUnavailable);
            modelBuilder.Entity<Package>().Ignore(p => p.FormattedPrice);
            modelBuilder.Entity<Package>().Ignore(p => p.IsOfferActive);
            modelBuilder.Entity<Package>().Ignore(p => p.OfferStatus);
            modelBuilder.Entity<DetailPackage>().Ignore(dp => dp.CostPerPerson);
            modelBuilder.Entity<Country>().Ignore(c => c.FlagUrl);
            modelBuilder.Entity<Destination>().Ignore(d => d.FullAddress);
            modelBuilder.Entity<Vehicle>().Ignore(v => v.FullVehicleName);
            modelBuilder.Entity<Vehicle>().Ignore(v => v.TransmissionLabel);
            modelBuilder.Entity<Hotel>().Ignore(h => h.StarDisplay);
            modelBuilder.Entity<Airport>().Ignore(a => a.DisplayName);
            modelBuilder.Entity<Flight>().Ignore(f => f.Duration);
            modelBuilder.Entity<Flight>().Ignore(f => f.DurationDisplay);
            modelBuilder.Entity<Flight>().Ignore(f => f.RouteDisplay);
            modelBuilder.Entity<ReservationStatus>().Ignore(rs => rs.BadgeColor);
            modelBuilder.Entity<Reservation>().Ignore(r => r.TotalAmount);
            modelBuilder.Entity<Reservation>().Ignore(r => r.FormattedTotal);
            modelBuilder.Entity<Reservation>().Ignore(r => r.PassengerCount);
            modelBuilder.Entity<DetailReservation>().Ignore(dr => dr.Nights);
            modelBuilder.Entity<DetailReservation>().Ignore(dr => dr.DateRangeDisplay);
            modelBuilder.Entity<DetailReservation>().Ignore(dr => dr.ServiceTypeLabel);
            modelBuilder.Entity<Promotion>().Ignore(p => p.IsCurrentlyValid);
            modelBuilder.Entity<Promotion>().Ignore(p => p.DiscountDisplay);
            modelBuilder.Entity<Promotion>().Ignore(p => p.DaysRemaining);
            modelBuilder.Entity<PromotionDetail>().Ignore(pd => pd.AppliesTo);
            modelBuilder.Entity<ReservationPromotion>().Ignore(rp => rp.FormattedDiscount);
            modelBuilder.Entity<Passenger>().Ignore(p => p.FullName);
            modelBuilder.Entity<Passenger>().Ignore(p => p.Age);
            modelBuilder.Entity<Passenger>().Ignore(p => p.IsMinor);
            modelBuilder.Entity<FlightSeat>().Ignore(fs => fs.SeatLabel);
            modelBuilder.Entity<FlightSeat>().Ignore(fs => fs.SeatStatusColor);
            modelBuilder.Entity<PaymentStatus>().Ignore(ps => ps.BadgeColor);
            modelBuilder.Entity<PaymentMethod>().Ignore(pm => pm.IconName);
            modelBuilder.Entity<Payment>().Ignore(p => p.FormattedAmount);
            modelBuilder.Entity<Payment>().Ignore(p => p.PaymentDateDisplay);
            modelBuilder.Entity<PayPalPayment>();
            modelBuilder.Entity<Room>().Ignore(r => r.RoomLabel);
        }
    }


}

