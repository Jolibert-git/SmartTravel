using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateAirportRequestValidator : AbstractValidator<CreateAirportRequest>
    {
        public CreateAirportRequestValidator()
        {
            RuleFor(x => x.IdDestination).GreaterThan(0).WithMessage("Debe seleccionar un destino.");
            RuleFor(x => x.AirportName).NotEmpty().MaximumLength(45);
            RuleFor(x => x.CodeIata)
                .NotEmpty()
                .MaximumLength(5)
                .Matches("^[A-Za-z]+$").WithMessage("El código IATA solo puede contener letras.");
            RuleForEach(x => x.PhoneNumbers).MaximumLength(15);
        }
    }
}

