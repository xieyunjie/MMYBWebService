using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model
{
    public class ReqChargeFee
    {
        public string center_id { get; set; }
        public string hospital_id { get; set; }
        public string indi_id { get; set; }
        public string biz_type { get; set; }
        public string treatment_type { get; set; }
        public string reg_staff { get; set; }
        public string reg_man { get; set; }
        /// <summary>
        /// 就诊时间  YYYY-MM-DD HH:MI:SS
        /// </summary>
        public string diagnose_date { get; set; }
        public string diagnose { get; set; }
        public string in_disease_name { get; set; }
        public string save_flag { get; set; }
        public string last_balance { get; set; }
        public string recipe_no { get; set; }
        public string doctor_no { get; set; }
        public string doctor_name { get; set; }
        public string note { get; set; }
        public string serial_apply { get; set; }
        public string bill_no { get; set; }

        [Miscellaneous.IgnorePutData]
        public List<ReqChargeFeeDetail> details { get; set; }
    }
}
