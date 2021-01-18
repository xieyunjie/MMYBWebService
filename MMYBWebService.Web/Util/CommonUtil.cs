using FluentValidation;
using FluentValidation.Results;
using MMYBWebService.Web.Model;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Util
{
    public class CommonUtil
    {
        public static string Validate<T>(T t, AbstractValidator<T> validator, bool blnThrowEx = true)
        {
            ValidationResult results = validator.Validate(t);
            bool b = results.IsValid;

            if (b == true)
            {
                return "";
            }
            else
            {

                var ex = new ValidatorException(results.Errors);

                if (blnThrowEx == true)
                {
                    throw ex;
                }
                else
                {
                    return ex.Message;
                }
                
            }
        }
    }
}
