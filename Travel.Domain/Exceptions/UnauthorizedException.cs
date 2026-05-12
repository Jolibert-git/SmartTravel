using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message = "No está autenticado. Por favor inicie sesión.")
            : base(message, HttpStatusCode.Unauthorized)
        { }
    }

}
