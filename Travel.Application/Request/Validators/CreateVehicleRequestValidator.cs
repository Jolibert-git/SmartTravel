using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateVehicleRequestValidator : AbstractValidator<CreateVehicleRequest>
    {
        public CreateVehicleRequestValidator()
        {
            RuleFor(x => x.IdService).GreaterThan(0).WithMessage("Debe seleccionar un servicio.");
            RuleFor(x => x.IdDestination).GreaterThan(0).WithMessage("Debe seleccionar un destino.");
            RuleFor(x => x.Make).NotEmpty().MaximumLength(45);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(45);
            RuleFor(x => x.Transmission).MaximumLength(45).When(x => x.Transmission != null);
        }
    }
}
