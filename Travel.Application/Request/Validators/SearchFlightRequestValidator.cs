using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class SearchFlightRequestValidator : AbstractValidator<SearchFlightRequest>
    {
        public SearchFlightRequestValidator()
        {
            RuleFor(x => x.OriginAirportId).GreaterThan(0).WithMessage("Debe seleccionar el aeropuerto de origen.");
            RuleFor(x => x.ArriveAirportId)
                .GreaterThan(0).WithMessage("Debe seleccionar el aeropuerto de destino.")
                .NotEqual(x => x.OriginAirportId).WithMessage("El origen y destino no pueden ser iguales.");
            RuleFor(x => x.DepartureDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("La fecha de búsqueda no puede ser en el pasado.");
        }
    }
}
