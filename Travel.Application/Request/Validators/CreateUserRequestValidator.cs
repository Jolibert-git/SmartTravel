using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(50).WithMessage("El nombre no puede superar 50 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.")
                .MaximumLength(50).WithMessage("El apellido no puede superar 50 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El formato del email no es válido.")
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una mayúscula.")
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("El teléfono no puede superar 20 caracteres.")
                .When(x => x.PhoneNumber != null);

            RuleFor(x => x.IdRol)
                .GreaterThan(0).WithMessage("Debe asignar un rol válido.");
        }
    }
}
