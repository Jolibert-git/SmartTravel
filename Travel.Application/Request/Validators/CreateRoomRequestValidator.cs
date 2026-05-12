using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator()
        {
            RuleFor(x => x.IdHotel).GreaterThan(0).WithMessage("Debe seleccionar un hotel.");
            RuleFor(x => x.IdTypeRoom).GreaterThan(0).WithMessage("Debe seleccionar un tipo de habitación.");
            RuleFor(x => x.IdService).GreaterThan(0).WithMessage("Debe seleccionar un servicio.");
        }
    }
}
