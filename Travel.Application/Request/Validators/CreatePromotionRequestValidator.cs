using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreatePromotionRequestValidator : AbstractValidator<CreatePromotionRequest>
    {
        public CreatePromotionRequestValidator()
        {
            RuleFor(x => x.PromotionName).NotEmpty().MaximumLength(60);

            RuleFor(x => x.IdDiscountType).GreaterThan(0).WithMessage("Debe seleccionar un tipo de descuento.");

            RuleFor(x => x.DiscountValue).GreaterThan(0).WithMessage("El valor del descuento debe ser mayor a 0.");

            RuleFor(x => x.DateFrom)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("La promoción no puede iniciar en el pasado.");

            RuleFor(x => x.DateTo)
                .NotEmpty()
                .GreaterThan(x => x.DateFrom).WithMessage("La fecha de fin debe ser posterior a la de inicio.");

            RuleFor(x => x.MinPersons).GreaterThan(0).WithMessage("El mínimo de personas debe ser al menos 1.");

            RuleFor(x => x.Details).NotEmpty().WithMessage("La promoción debe aplicar a al menos un servicio o paquete.");

            RuleForEach(x => x.Details).ChildRules(d =>
            {
                d.RuleFor(x => x)
                    .Must(x => x.IdService.HasValue || x.IdPackage.HasValue)
                    .WithMessage("Cada detalle debe especificar un servicio o un paquete.");
            });
        }
    }
}
