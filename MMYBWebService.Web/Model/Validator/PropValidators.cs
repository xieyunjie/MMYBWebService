using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model.Validator
{
    public class DateTimeFormatValidator : PropertyValidator 
    {
        private string _format;
        public DateTimeFormatValidator(string format)
        {
            this._format = format;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            bool bln = DateTime.TryParseExact(context.PropertyValue as string, _format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime dt);
            if (bln == false)
            {
                context.MessageFormatter.AppendArgument("Format", this._format);
                return false;
            }
            return true;
        }

        protected override string GetDefaultMessageTemplate() => "{PropertyName}与时间格式{Format}不相符。";

        public static DateTimeFormatValidator LongDateTimeValidator()
        {
            return new DateTimeFormatValidator("yyyy-MM-dd HH:mm:ss");
        }

        public static DateTimeFormatValidator ShortDateTimeValidator()
        {
            return new DateTimeFormatValidator("yyyy-MM-dd");
        }
    }
}
