using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    /// <summary>
    /// 上次住院业务信息
    /// </summary>
    public class ResLastBizInfo
    {
        public string hospital_id { get; set; }
        public string biz_type { get; set; }
        public string center_id { get; set; }
        public string indi_id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string idcard { get; set; }
        public string ic_no { get; set; }
        public string birthday { get; set; }
        public string telephone { get; set; }
        public string corp_id { get; set; }
        public string corp_name { get; set; }
        public string treatment_type { get; set; }
        /// <summary>
        /// 业务登记日期 YYYY-MM-DD hh:mm:ss
        /// </summary>
        public string reg_date { get; set; }
        public string reg_staff { get; set; }
        public string reg_man { get; set; }
        public string reg_flag { get; set; }
        /// <summary>
        /// YYYY-MM-DD
        /// </summary>
        public string begin_date { get; set; }
        public string reg_info { get; set; }
        public string in_dept { get; set; }
        public string in_dept_name { get; set; }
        public string in_area { get; set; }
        public string in_area_name { get; set; }
        public string in_bed { get; set; }
        public string patient_id { get; set; }
        public string in_disease { get; set; }
        public string disease { get; set; } 
    }
}
