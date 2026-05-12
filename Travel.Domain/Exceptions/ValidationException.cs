using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Exceptions
{
    public class ValidationException : AppException
    {
        /// <summary>
        /// Errores agrupados por campo.
        /// Key = nombre del campo, Value = lista de mensajes de error.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Se encontraron errores de validación.", HttpStatusCode.UnprocessableEntity)
        {
            Errors = errors;
        }

        public ValidationException(string field, string error)
            : base("Se encontraron errores de validación.", HttpStatusCode.UnprocessableEntity)
        {
            Errors = new Dictionary<string, string[]>
            {
                { field, new[] { error } }
            };
        }
    }
}
