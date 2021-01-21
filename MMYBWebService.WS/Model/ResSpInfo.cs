using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    /// <summary>
    /// 业务申请信息
    /// </summary>
    public class ResSpInfo
    {
        public string serial_apply { get; set; }
        public string biz_type { get; set; }
        public string biz_name { get; set; }
        public string apply_content { get; set; }
        public string apply_content_name { get; set; }
        public string treatment_type { get; set; }
        public string treatment_name { get; set; }
        /// <summary>
        /// 申请生效日期 yyyy-MM-dd
        /// </summary>
        public string admit_effect { get; set; }
        /// <summary>
        /// 申请失效日期  yyyy-MM-dd
        /// </summary>
        public string admit_date { get; set; }
        public string icd { get; set; }
        public string disease { get; set; }
        public string injury_borth_sn { get; set; }
        public string akc030 { get; set; } 
    }
}
