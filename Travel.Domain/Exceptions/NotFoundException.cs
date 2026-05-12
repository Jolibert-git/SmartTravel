using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string resource, object identifier)
            : base($"{resource} con identificador '{identifier}' no fue encontrado.", HttpStatusCode.NotFound)
        { }

        public NotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        { }
    }
}
