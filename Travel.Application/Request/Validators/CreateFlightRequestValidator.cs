using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateFlightRequestValidator : AbstractValidator<CreateFlightRequest>
    {
        public CreateFlightRequestValidator()
        {
            RuleFor(x => x.IdService).GreaterThan(0).WithMessage("Debe seleccionar un servicio.");

            RuleFor(x => x.DateDeparture)
                .NotEmpty().WithMessage("La fecha de salida es requerida.")
                .GreaterThan(DateTime.Now).WithMessage("La fecha de salida debe ser futura.");

            RuleFor(x => x.DateArrival)
                .NotEmpty().WithMessage("La fecha de llegada es requerida.")
                .GreaterThan(x => x.DateDeparture).WithMessage("La llegada debe ser posterior a la salida.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0.");

            RuleFor(x => x.AirportIdOrigen)
                .GreaterThan(0).WithMessage("Debe seleccionar el aeropuerto de origen.");

            RuleFor(x => x.AirportIdArrive)
                .GreaterThan(0).WithMessage("Debe seleccionar el aeropuerto de destino.")
                .NotEqual(x => x.AirportIdOrigen).WithMessage("El aeropuerto de origen y destino no pueden ser el mismo.");
        }
    }
}
