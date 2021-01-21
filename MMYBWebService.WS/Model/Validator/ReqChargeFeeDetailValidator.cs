using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model.Validator
{
    public class ReqChargeFeeDetailValidator : AbstractValidator<ReqChargeFeeDetail>
    {
        public ReqChargeFeeDetailValidator()
        {
            RuleFor(x => x.medi_item_type).Length(1);
            RuleFor(x => x.stat_type).Length(0, 3);
            RuleFor(x => x.his_item_code).Length(0, 20);
            RuleFor(x => x.item_code).Length(0, 20);
            RuleFor(x => x.his_item_name).Length(0, 50);
            //RuleFor(x => x.model).Length(0, 30);
            //RuleFor(x => x.factory).Length(0, 50);
            //RuleFor(x => x.standard).Length(0, 30);
            RuleFor(x => x.fee_date).SetValidator(DateTimeFormatValidator.ShortDateTimeValidator());
            //RuleFor(x => x.unit).Length(0, 10);
            RuleFor(x => x.price).Length(0, 12);
            //RuleFor(x => x.dosage).Length(0, 12);
            RuleFor(x => x.money).Length(0, 12);
            RuleFor(x => x.usage_flag).Length(1);
            //RuleFor(x => x.usage_days).Length(0, 3);
            //RuleFor(x => x.opp_serial_fee).Length(0, 12);
            //RuleFor(x => x.hos_serial).Length(0, 20);
            RuleFor(x => x.input_staff).Length(0, 20);
            RuleFor(x => x.input_man).Length(0, 30);
            RuleFor(x => x.input_date).SetValidator(DateTimeFormatValidator.LongDateTimeValidator());
            //RuleFor(x => x.recipe_no).Length(0, 20);
            //RuleFor(x => x.doctor_no).Length(0, 8);
            //RuleFor(x => x.doctor_name).Length(0, 10); 
        }
    }
}
