using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request.Validators
{
    public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
    {
        public CreatePaymentRequestValidator()
        {
            RuleFor(x => x.IdReservation).GreaterThan(0).WithMessage("Debe especificar una reserva válida.");
            RuleFor(x => x.IdPaymentMethod).GreaterThan(0).WithMessage("Debe seleccionar un método de pago.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("El monto debe ser mayor a 0.");
        }
    }

}
