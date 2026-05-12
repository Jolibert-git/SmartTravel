using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateSupplierRequestValidator : AbstractValidator<CreateSupplierRequest>
    {
        public CreateSupplierRequestValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("El nombre de la empresa es requerido.")
                .MaximumLength(45);

            RuleFor(x => x.Rnc)
                .NotEmpty().WithMessage("El RNC es requerido.")
                .MaximumLength(15).WithMessage("El RNC no puede superar 15 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El formato del email no es válido.")
                .MaximumLength(60);

            RuleForEach(x => x.PhoneNumbers)
                .MaximumLength(15).WithMessage("Cada teléfono no puede superar 15 caracteres.");
        }
    }
}
