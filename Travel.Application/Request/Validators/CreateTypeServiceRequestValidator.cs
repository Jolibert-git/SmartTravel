using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateTypeServiceRequestValidator : AbstractValidator<CreateTypeServiceRequest>
    {
        public CreateTypeServiceRequestValidator()
        {
            RuleFor(x => x.TypeDescription).NotEmpty().MaximumLength(30);
        }
    }
}
