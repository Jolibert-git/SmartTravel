using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateDestinationRequestValidator : AbstractValidator<CreateDestinationRequest>
    {
        public CreateDestinationRequestValidator()
        {
            RuleFor(x => x.IdCountry).GreaterThan(0).WithMessage("Debe seleccionar un país.");
            RuleFor(x => x.City).NotEmpty().MaximumLength(45);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(45);
        }
    }
}
