using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message)
            : base(message, HttpStatusCode.Conflict)
        { }

        public ConflictException(string resource, string field, object value)
            : base($"{resource} con {field} '{value}' ya existe.", HttpStatusCode.Conflict)
        { }
    }
}
