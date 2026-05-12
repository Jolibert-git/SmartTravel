using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validattors
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().WithMessage("El access token es requerido.");
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("El refresh token es requerido.");
        }
    }
}
