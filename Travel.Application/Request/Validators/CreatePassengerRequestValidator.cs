using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreatePassengerRequestValidator : AbstractValidator<CreatePassengerRequest>
    {
        public CreatePassengerRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
                .LessThan(DateTime.Today).WithMessage("La fecha de nacimiento debe ser en el pasado.")
                .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("La fecha de nacimiento no es válida.");

            RuleFor(x => x.DocumentNumber)
                .NotEmpty().WithMessage("El número de documento es requerido.")
                .MaximumLength(20);

            RuleFor(x => x.IdDocumentType).GreaterThan(0).WithMessage("Debe seleccionar un tipo de documento.");
            RuleFor(x => x.IdCountry).GreaterThan(0).WithMessage("Debe seleccionar un país.");
        }
    }
}
