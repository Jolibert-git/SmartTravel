using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreatePaymentRequest
    {
        public long IdReservation { get; set; }
        public long IdPaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
