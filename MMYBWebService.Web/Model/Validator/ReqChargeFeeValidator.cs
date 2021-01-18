using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model.Validator
{
    public class ReqChargeFeeValidator : AbstractValidator<ReqChargeFee>
    {
        public ReqChargeFeeValidator()
        {
            RuleFor(x => x.center_id).Length(0, 10);
            RuleFor(x => x.hospital_id).Length(0, 20);
            RuleFor(x => x.indi_id).Length(0, 12);
            RuleFor(x => x.biz_type).Length(2);
            RuleFor(x => x.treatment_type).Length(0, 3);
            RuleFor(x => x.reg_staff).Length(0, 5);
            RuleFor(x => x.reg_man).Length(0, 10);
            RuleFor(x => x.diagnose_date).SetValidator(DateTimeFormatValidator.LongDateTimeValidator());
            RuleFor(x => x.diagnose).Length(0, 20);
            //RuleFor(x => x.in_disease_name).Length(10);
            RuleFor(x => x.save_flag).Length(1);
            RuleFor(x => x.last_balance).Length(0, 18);
            RuleFor(x => x.recipe_no).Length(0, 20);
            RuleFor(x => x.doctor_no).Length(0, 12);
            RuleFor(x => x.doctor_no).Length(0, 20);
            //RuleFor(x => x.note).Length(0,100);
            RuleFor(x => x.serial_apply).Length(0, 12);
            //RuleFor(x => x.bill_no).Length(0, 18); 
            RuleFor(x => x.feeinfo).Must(list => list.Count > 0).WithMessage("没有费用明细数据！");
            RuleForEach(x => x.feeinfo).SetValidator(new ReqChargeFeeDetailValidator());
        }
    }
}
