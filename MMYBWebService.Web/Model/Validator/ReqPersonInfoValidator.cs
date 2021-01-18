using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model.Validator
{
    public class ReqPersonInfoValidator: AbstractValidator<ReqPersonInfo>
    {
        public ReqPersonInfoValidator()
        {
            RuleFor(x => x.idcard).NotEmpty().Length(15,25);
            RuleFor(x => x.hospital_id).NotEmpty().Length(0,20);
            RuleFor(x => x.biz_type).NotEmpty().Length(2);
        }
    }
}
