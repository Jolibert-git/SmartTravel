using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class UpdateReservationStatusRequestValidator : AbstractValidator<UpdateReservationStatusRequest>
    {
        public UpdateReservationStatusRequestValidator()
        {
            RuleFor(x => x.IdReservationStatus)
                .GreaterThan(0).WithMessage("Debe seleccionar un estado válido.");
        }
    }
}
