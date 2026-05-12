using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
    {
        public CreateServiceRequestValidator()
        {
            RuleFor(x => x.ServiceDescription)
                .NotEmpty().WithMessage("La descripción del servicio es requerida.")
                .MaximumLength(45);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

            RuleFor(x => x.IdTypeService)
                .GreaterThan(0).WithMessage("Debe seleccionar un tipo de servicio.");

            RuleFor(x => x.IdSupplier)
                .GreaterThan(0).WithMessage("Debe seleccionar un proveedor.");
        }
    }
}
