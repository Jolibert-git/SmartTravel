using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Exceptions
{
    public class BusinessRuleException : AppException
    {
        /// <summary>Código corto de la regla violada (útil para el frontend).</summary>
        public string RuleCode { get; }

        public BusinessRuleException(string message, string ruleCode = "BUSINESS_RULE_VIOLATION")
            : base(message, HttpStatusCode.UnprocessableEntity)
        {
            RuleCode = ruleCode;
        }
    }
}
