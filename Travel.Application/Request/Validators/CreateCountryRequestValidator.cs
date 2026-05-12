using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateCountryRequestValidator : AbstractValidator<CreateCountryRequest>
    {
        public CreateCountryRequestValidator()
        {
            RuleFor(x => x.CountryName).NotEmpty().MaximumLength(60);
            RuleFor(x => x.IsoCode)
                .NotEmpty()
                .Length(2).WithMessage("El código ISO debe tener exactamente 2 caracteres.")
                .Matches("^[A-Za-z]+$").WithMessage("El código ISO solo puede contener letras.");
        }
    }
}
