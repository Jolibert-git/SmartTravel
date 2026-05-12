using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreatePackageRequestValidator : AbstractValidator<CreatePackageRequest>
    {
        public CreatePackageRequestValidator()
        {
            RuleFor(x => x.PackageName)
                .NotEmpty().WithMessage("El nombre del paquete es requerido.")
                .MaximumLength(60);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

            RuleFor(x => x.OfferStart)
                .NotEmpty().WithMessage("La fecha de inicio de la oferta es requerida.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("La oferta no puede iniciar en el pasado.");

            RuleFor(x => x.OfferEnd)
                .NotEmpty().WithMessage("La fecha de fin de la oferta es requerida.")
                .GreaterThan(x => x.OfferStart).WithMessage("La fecha de fin debe ser posterior a la de inicio.");

            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("El paquete debe tener al menos un servicio.");

            RuleForEach(x => x.Details).ChildRules(d =>
            {
                d.RuleFor(x => x.NumberPersons).GreaterThan(0).WithMessage("El número de personas debe ser mayor a 0.");
                d.RuleFor(x => x.CostPrice).GreaterThan(0).WithMessage("El costo debe ser mayor a 0.");
                d.RuleFor(x => x.IdService).GreaterThan(0).WithMessage("Debe seleccionar un servicio válido.");
            });
        }
    }
}
