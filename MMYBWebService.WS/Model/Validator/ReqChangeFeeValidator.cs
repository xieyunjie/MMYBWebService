using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model.Validator
{
    public class ReqChangeFeeValidator : AbstractValidator<ReqChangeFee>
    {
        public ReqChangeFeeValidator()
        {
            RuleFor(x => x.center_id).Length(0, 10);
            RuleFor(x => x.hospital_id).Length(0, 20);
            RuleFor(x => x.indi_id).Length(0, 12);
            RuleFor(x => x.biz_type).Length(2);
            RuleFor(x => x.treatment_type).Length(0, 3);
            RuleFor(x => x.reg_staff).Length(0, 5);
            RuleFor(x => x.reg_man).Length(0, 10);
            RuleFor(x => x.save_flag).Length(1); 
            RuleFor(x => x.feeinfo).Must(list => list.Count > 0).WithMessage("费用明细不能为零个！"); 
            RuleForEach(x => x.feeinfo).SetValidator(new ReqChargeFeeDetailValidator());
        }
    }
}
