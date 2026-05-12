using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message = "No tiene permisos para realizar esta acción.")
            : base(message, HttpStatusCode.Forbidden)
        { }
    }
}
