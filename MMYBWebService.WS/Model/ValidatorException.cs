using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    public class ValidatorException : Exception
    {
        public ValidatorException(IList<ValidationFailure> failures) : base(string.Join("", failures.Select(x => x.ErrorMessage)))
        {
        }
    }
}
