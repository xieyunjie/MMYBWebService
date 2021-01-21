using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    /// <summary>
    /// 个人基本信息
    /// </summary>
    public class ResPersonInfo
    {
        public string indi_id { get; set; }
        public string center_id { get; set; }
        public string center_name { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string pers_type { get; set; }
        public string pers_name { get; set; }
        public string indi_join_sta { get; set; }
        public string indi_sta_name { get; set; }
        public string official_code { get; set; }
        public string official_name { get; set; }
        public string hire_type { get; set; }
        public string hire_name { get; set; }
        public string idcard { get; set; }
        public string insr_code { get; set; }
        public string telephone { get; set; }
        /// <summary>
        /// 出生日期 yyyy-MM-dd
        /// </summary>
        public string birthday { get; set; }
        public string post_code { get; set; }
        public string corp_id { get; set; }
        public string corp_name { get; set; }
        public string insr_detail_code { get; set; }
        public string insr_detail_name { get; set; }
        /// <summary>
        /// 保险类型编号
        /// </summary>
        public string insur_no { get; set; } 
    }
}
