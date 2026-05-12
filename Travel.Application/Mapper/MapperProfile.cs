using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Travel.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // ----------------------------------------------------------------
            // Catálogos simples
            // ----------------------------------------------------------------
            CreateMap<Rol, RolDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.DisplayName));

            CreateMap<TypeService, TypeServiceDto>();
            CreateMap<TypeRoom, TypeRoomDto>();
            CreateMap<DocumentType, DocumentTypeDto>();
            CreateMap<SeatClass, SeatClassDto>();
            CreateMap<DiscountType, DiscountTypeDto>();

            // ----------------------------------------------------------------
            // User
            // ----------------------------------------------------------------
            CreateMap<SystemsUser, UserResponseDto>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName))
                .ForMember(d => d.Initials, o => o.MapFrom(s => s.Initials))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.RolUsers.Select(ru => ru.Rol)));

            CreateMap<SystemsUser, UserSummaryDto>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName))
                .ForMember(d => d.Initials, o => o.MapFrom(s => s.Initials));

            // ----------------------------------------------------------------
            // Supplier
            // ----------------------------------------------------------------
            CreateMap<Supplier, SupplierResponseDto>()
                .ForMember(d => d.PhoneNumbers,
                    o => o.MapFrom(s => s.PhoneSuppliers.Select(p => p.PhoneNumber).ToList()));

            // ----------------------------------------------------------------
            // OfferedService
            // ----------------------------------------------------------------
            CreateMap<OfferedService, ServiceResponseDto>()
                .ForMember(d => d.FormattedPrice, o => o.MapFrom(s => s.FormattedPrice))
                .ForMember(d => d.CurrentStatus,
                    o => o.MapFrom(s => s.ServiceAvailabilities
                        .OrderByDescending(sa => sa.DateFrom)
                        .FirstOrDefault()!.AvailabilityStatus.StatusDescription));

            // ----------------------------------------------------------------
            // Country & Destination
            // ----------------------------------------------------------------
            CreateMap<Country, CountryDto>()
                .ForMember(d => d.FlagUrl, o => o.MapFrom(s => s.FlagUrl));

            CreateMap<Destination, DestinationResponseDto>()
                .ForMember(d => d.FullAddress, o => o.MapFrom(s => s.FullAddress));

            // ----------------------------------------------------------------
            // Vehicle
            // ----------------------------------------------------------------
            CreateMap<Vehicle, VehicleResponseDto>()
                .ForMember(d => d.FullVehicleName, o => o.MapFrom(s => s.FullVehicleName))
                .ForMember(d => d.TransmissionLabel, o => o.MapFrom(s => s.TransmissionLabel));

            // ----------------------------------------------------------------
            // Hotel & Room
            // ----------------------------------------------------------------
            CreateMap<Hotel, HotelResponseDto>()
                .ForMember(d => d.StarDisplay, o => o.MapFrom(s => s.StarDisplay))
                .ForMember(d => d.PhoneNumbers,
                    o => o.MapFrom(s => s.PhoneHotels.Select(p => p.PhoneNumber).ToList()));

            CreateMap<Room, RoomResponseDto>()
                .ForMember(d => d.RoomLabel, o => o.MapFrom(s => s.RoomLabel));

            // ----------------------------------------------------------------
            // Airport & Flight
            // ----------------------------------------------------------------
            CreateMap<Airport, AirportResponseDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.DisplayName))
                .ForMember(d => d.PhoneNumbers,
                    o => o.MapFrom(s => s.PhoneAirports.Select(p => p.PhoneNumber).ToList()));

            CreateMap<Flight, FlightResponseDto>()
                .ForMember(d => d.DurationDisplay, o => o.MapFrom(s => s.DurationDisplay))
                .ForMember(d => d.RouteDisplay, o => o.MapFrom(s => s.RouteDisplay))
                .ForMember(d => d.AvailableSeats,
                    o => o.MapFrom(s => s.FlightSeats.Count(fs => fs.IsAvailable)));

            CreateMap<FlightSeat, FlightSeatResponseDto>()
                .ForMember(d => d.SeatLabel, o => o.MapFrom(s => s.SeatLabel))
                .ForMember(d => d.SeatStatusColor, o => o.MapFrom(s => s.SeatStatusColor));

            // ----------------------------------------------------------------
            // Package
            // ----------------------------------------------------------------
            CreateMap<Package, PackageResponseDto>()
                .ForMember(d => d.FormattedPrice, o => o.MapFrom(s => s.FormattedPrice))
                .ForMember(d => d.IsOfferActive, o => o.MapFrom(s => s.IsOfferActive))
                .ForMember(d => d.OfferStatus, o => o.MapFrom(s => s.OfferStatus))
                .ForMember(d => d.Details, o => o.MapFrom(s => s.DetailPackages));

            CreateMap<DetailPackage, PackageDetailDto>()
                .ForMember(d => d.CostPerPerson, o => o.MapFrom(s => s.CostPerPerson));

            // ----------------------------------------------------------------
            // Passenger
            // ----------------------------------------------------------------
            CreateMap<Passenger, PassengerResponseDto>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName))
                .ForMember(d => d.Age, o => o.MapFrom(s => s.Age))
                .ForMember(d => d.IsMinor, o => o.MapFrom(s => s.IsMinor));

            // ----------------------------------------------------------------
            // Promotion
            // ----------------------------------------------------------------
            CreateMap<Promotion, PromotionResponseDto>()
                .ForMember(d => d.DiscountDisplay, o => o.MapFrom(s => s.DiscountDisplay))
                .ForMember(d => d.IsCurrentlyValid, o => o.MapFrom(s => s.IsCurrentlyValid))
                .ForMember(d => d.DaysRemaining, o => o.MapFrom(s => s.DaysRemaining))
                .ForMember(d => d.Details, o => o.MapFrom(s => s.PromotionDetails));

            CreateMap<PromotionDetail, PromotionDetailDto>()
                .ForMember(d => d.AppliesTo, o => o.MapFrom(s => s.AppliesTo));

            // ----------------------------------------------------------------
            // Reservation
            // ----------------------------------------------------------------
            CreateMap<Reservation, ReservationResponseDto>()
                .ForMember(d => d.StatusDescription, o => o.MapFrom(s => s.ReservationStatus.StatusDescription))
                .ForMember(d => d.BadgeColor, o => o.MapFrom(s => s.ReservationStatus.BadgeColor))
                .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.TotalAmount))
                .ForMember(d => d.FormattedTotal, o => o.MapFrom(s => s.FormattedTotal))
                .ForMember(d => d.PassengerCount, o => o.MapFrom(s => s.PassengerCount))
                .ForMember(d => d.Details, o => o.MapFrom(s => s.DetailReservations))
                .ForMember(d => d.Passengers, o => o.MapFrom(s => s.ReservationPassengers.Select(rp => rp.Passenger)))
                .ForMember(d => d.Promotions, o => o.MapFrom(s => s.ReservationPromotions));

            CreateMap<Reservation, ReservationSummaryDto>()
                .ForMember(d => d.StatusDescription, o => o.MapFrom(s => s.ReservationStatus.StatusDescription))
                .ForMember(d => d.BadgeColor, o => o.MapFrom(s => s.ReservationStatus.BadgeColor))
                .ForMember(d => d.FormattedTotal, o => o.MapFrom(s => s.FormattedTotal))
                .ForMember(d => d.PassengerCount, o => o.MapFrom(s => s.PassengerCount));

            CreateMap<DetailReservation, ReservationDetailDto>()
                .ForMember(d => d.DateRangeDisplay, o => o.MapFrom(s => s.DateRangeDisplay))
                .ForMember(d => d.Nights, o => o.MapFrom(s => s.Nights))
                .ForMember(d => d.ServiceTypeLabel, o => o.MapFrom(s => s.ServiceTypeLabel));

            CreateMap<ReservationPromotion, ReservationPromotionDto>()
                .ForMember(d => d.PromotionName, o => o.MapFrom(s => s.Promotion.PromotionName))
                .ForMember(d => d.FormattedDiscount, o => o.MapFrom(s => s.FormattedDiscount));

            // ----------------------------------------------------------------
            // Payment
            // ----------------------------------------------------------------
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(d => d.FormattedAmount, o => o.MapFrom(s => s.FormattedAmount))
                .ForMember(d => d.PaymentDateDisplay, o => o.MapFrom(s => s.PaymentDateDisplay))
                .ForMember(d => d.StatusDescription, o => o.MapFrom(s => s.PaymentStatus.StatusDescription))
                .ForMember(d => d.BadgeColor, o => o.MapFrom(s => s.PaymentStatus.BadgeColor))
                .ForMember(d => d.MethodName, o => o.MapFrom(s => s.PaymentMethod.MethodName))
                .ForMember(d => d.IconName, o => o.MapFrom(s => s.PaymentMethod.IconName));

            CreateMap<PaymentMethod, PaymentMethodDto>()
                .ForMember(d => d.IconName, o => o.MapFrom(s => s.IconName));
        }
    }
}


