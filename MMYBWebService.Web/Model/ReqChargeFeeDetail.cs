using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model
{
    public class ReqChargeFeeDetail
    {
        public string medi_item_type { get; set; }
        public string stat_type { get; set; }
        public string his_item_code { get; set; }
        public string item_code { get; set; }
        public string his_item_name { get; set; }
        public string model { get; set; }
        public string factory { get; set; }
        public string standard { get; set; }
        public string fee_date { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string dosage { get; set; }
        public string money { get; set; }
        public string usage_flag { get; set; }
        public string usage_days { get; set; }
        public string opp_serial_fee { get; set; }
        public string hos_serial { get; set; }
        public string input_staff { get; set; }
        public string input_man { get; set; }
        /// <summary>
        /// yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string input_date { get; set; }
        public string recipe_no { get; set; }
        public string doctor_no { get; set; }
        public string doctor_name { get; set; }
    }
}
