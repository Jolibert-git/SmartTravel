using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("La contraseña actual es requerida.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("La nueva contraseña es requerida.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("Debe contener al menos una mayúscula.")
                .Matches("[0-9]").WithMessage("Debe contener al menos un número.")
                .NotEqual(x => x.CurrentPassword).WithMessage("La nueva contraseña no puede ser igual a la actual.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Las contraseñas no coinciden.");
        }
    }
}
