using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateHotelRequestValidator : AbstractValidator<CreateHotelRequest>
    {
        public CreateHotelRequestValidator()
        {
            RuleFor(x => x.IdDestination).GreaterThan(0).WithMessage("Debe seleccionar un destino.");

            RuleFor(x => x.HotelName)
                .NotEmpty().WithMessage("El nombre del hotel es requerido.")
                .MaximumLength(45);

            RuleFor(x => x.Stars)
                .InclusiveBetween(1, 5).WithMessage("Las estrellas deben estar entre 1 y 5.")
                .When(x => x.Stars.HasValue);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("El formato del email no es válido.")
                .MaximumLength(60)
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleForEach(x => x.PhoneNumbers).MaximumLength(15);
        }
    }
}
