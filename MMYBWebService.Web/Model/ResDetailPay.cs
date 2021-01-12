using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model
{
    public class ResDetailPay
    {
        public string hospital_id { get; set; }
        public string label_flag { get; set; }
        public string fee_batch { get; set; }
        public string fund_id { get; set; }
        public string serial_pay { get; set; }
        public string pay_date { get; set; }
        public string policy_item_code { get; set; }
        public string serial_fee { get; set; }
        public string real_pay { get; set; }
        public string serial_no { get; set; }
        public string trans_flag { get; set; }
        public string pay_ordinal { get; set; } 
    }
}
