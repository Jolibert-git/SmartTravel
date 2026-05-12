using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequest>
    {
        public UpdateServiceRequestValidator()
        {
            RuleFor(x => x.ServiceDescription).NotEmpty().MaximumLength(45);
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
        }
    }
}
