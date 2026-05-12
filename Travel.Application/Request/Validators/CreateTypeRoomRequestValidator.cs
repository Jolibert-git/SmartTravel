using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateTypeRoomRequestValidator : AbstractValidator<CreateTypeRoomRequest>
    {
        public CreateTypeRoomRequestValidator()
        {
            RuleFor(x => x.TypeDescription).NotEmpty().MaximumLength(45);
        }
    }
}
