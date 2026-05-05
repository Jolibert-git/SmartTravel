using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Exceptions
{
    public class PaymentException : AppException
    {
        public PaymentException(string message)
            : base(message, HttpStatusCode.PaymentRequired) // 402 error
        { }
    }
}
