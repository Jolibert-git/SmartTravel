using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class UpdateSupplierRequestValidator : AbstractValidator<UpdateSupplierRequest>
    {
        public UpdateSupplierRequestValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(45);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(60);
            RuleForEach(x => x.PhoneNumbers).MaximumLength(15);
        }
    }
}
