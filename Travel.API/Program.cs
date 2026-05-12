
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.MicrosoftExtensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Travel.Application.Contracts;
using Travel.Application.Mapper;
using Travel.Application.Request;
using Travel.Application.Services;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Infrastructure.Repositories;
using Travel.Persistence.Context;

namespace Travel.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")//,
                    //sql => sql.EnableRetryOnFailure(
                    //    maxRetryCount: 3,
                    //    maxRetryDelay: TimeSpan.FromSeconds(5),
                    //    errorNumbersToAdd: null
                    //)
                )
            );


            builder.Services.AddScoped<DbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Catalogs & Statuses
            builder.Services.AddScoped<IRolRepository, RolRepository>();
            builder.Services.AddScoped<ITypeServiceRepository, TypeServiceRepository>();
            builder.Services.AddScoped<ITypeRoomRepository, TypeRoomRepository>();
            builder.Services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            builder.Services.AddScoped<IAvailabilityStatusRepository, AvailabilityStatusRepository>();
            builder.Services.AddScoped<IReservationStatusRepository, ReservationStatusRepository>();
            builder.Services.AddScoped<IReservationPassengerRepository, ReservationPassengerRepository>();
            builder.Services.AddScoped<IDiscountTypeRepository, DiscountTypeRepository>();
            builder.Services.AddScoped<ISeatClassRepository, SeatClassRepository>();
            builder.Services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
            builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            builder.Services.AddHttpClient<IPaypalService, PayPalService>();


            // Users
            builder.Services.AddScoped<ISystemsUserRepository, SystemsUserRepository>();

            // Suppliers & Services
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IPhoneSupplierRepository, PhoneSupplierRepository>();
            builder.Services.AddScoped<IOfferedServiceRepository, OfferedServiceRepository>();
            builder.Services.AddScoped<IServiceAvailabilityRepository, ServiceAvailabilityRepository>();

            // Geography
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();

            // Hotels
            builder.Services.AddScoped<IHotelRepository, HotelRepository>();
            builder.Services.AddScoped<IPhoneHotelRepository, PhoneHotelRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();

            // Airports & Flights
            builder.Services.AddScoped<IAirportRepository, AirportRepository>();
            builder.Services.AddScoped<IPhoneAirportRepository, PhoneAirportRepository>();
            builder.Services.AddScoped<IFlightRepository, FlightRepository>();
            builder.Services.AddScoped<IFlightSeatRepository, FlightSeatRepository>();

            // Vehicles
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

            // Packages
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IDetailPackageRepository, DetailPackageRepository>();

            // Promotions
            builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
            builder.Services.AddScoped<IPromotionDetailRepository, PromotionDetailRepository>();

            // Passengers
            builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();

            // Reservations
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IDetailReservationRepository, DetailReservationRepository>();
            builder.Services.AddScoped<IReservationPromotionRepository, ReservationPromotionRepository>();
            builder.Services.AddScoped<IFlightSeatReservationRepository, FlightSeatReservationRepository>();

            // Payments
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPayPalPaymentRepository, PayPalPaymentRepository>();

            

            builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            



            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();




            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICatalogService, CatalogService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IServiceManagementService, ServiceManagementService>();
            builder.Services.AddScoped<IDestinationService, DestinationService>();
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<IPassengerService, PassengerService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddHttpClient<IPaypalService, PayPalService>();
            builder.Services.AddScoped<IPromotionService, PromotionService>();
            builder.Services.AddScoped<IAirportService, AirportService>();



            var jwtKey = builder.Configuration["Jwt:Key"]!;
            var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
            var jwtAudience = builder.Configuration["Jwt:Audience"]!;


             builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = jwtAudience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero   // sin margen de tolerancia al expirar
                    };



                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsJsonAsync(new
                            {
                                success = false,
                                statusCode = 401,
                                message = "No está autenticado. Por favor inicie sesión."
                            });
                        },
                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsJsonAsync(new
                            {
                                success = false,
                                statusCode = 403,
                                message = "No tiene permisos para realizar esta acción."
                            });
                        }
                    };
                });


            builder.Services.AddAuthorization();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevCors", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    // camelCase en todas las respuestas
                    options.JsonSerializerOptions.PropertyNamingPolicy =
                        System.Text.Json.JsonNamingPolicy.CamelCase;

                    // Ignorar nulos para no enviar campos vacíos al frontend
                    options.JsonSerializerOptions.DefaultIgnoreCondition =
                         System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });






            //builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();


            


            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TravelReservation API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese: Bearer {su token JWT}"
                });



                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });


            });
            












            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelReservation API v1");
                    c.RoutePrefix = string.Empty;   // Swagger en la raíz: https://localhost:PORT/
                });
                app.MapOpenApi();
            }

            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
