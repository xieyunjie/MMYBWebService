using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    public class ReqChangeFee
    {
        public string center_id { get; set; }
        public string hospital_id { get; set; }
        public string serial_no { get; set; }
        public string indi_id { get; set; }
        public string biz_type { get; set; }
        public string treatment_type { get; set; }
        public string reg_staff { get; set; }
        public string reg_man { get; set; } 
        public string save_flag { get; set; }
        public string bill_no { get; set; }
        public string trade_no { get; set; }

        [Miscellaneous.IgnorePutData]
        public List<ReqChargeFeeDetail> feeinfo { get; set; }
    }
}
