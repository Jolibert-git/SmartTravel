using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
    {
        public CreateReservationRequestValidator()
        {
            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("La reserva debe tener al menos un servicio.");

            RuleFor(x => x.PassengerIds)
                .NotEmpty().WithMessage("La reserva debe tener al menos un pasajero.");

            RuleForEach(x => x.Details).ChildRules(d =>
            {
                d.RuleFor(x => x.DateCheckIn)
                    .NotEmpty().WithMessage("La fecha de check-in es requerida.")
                    .GreaterThanOrEqualTo(DateTime.Today).WithMessage("El check-in no puede ser en el pasado.");

                d.RuleFor(x => x.DateCheckOut)
                    .NotEmpty().WithMessage("La fecha de check-out es requerida.")
                    .GreaterThan(x => x.DateCheckIn).WithMessage("El check-out debe ser posterior al check-in.");

                d.RuleFor(x => x.IdService)
                    .GreaterThan(0).WithMessage("Debe seleccionar un servicio válido.");

                d.RuleFor(x => x)
                    .Must(x => x.IdRoom.HasValue || x.IdFlight.HasValue || x.IdVehicle.HasValue)
                    .WithMessage("Cada detalle debe especificar habitación, vuelo o vehículo.");
            });
        }
    }
}
