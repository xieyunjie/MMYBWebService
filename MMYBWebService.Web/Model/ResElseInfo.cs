using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model
{
    /// <summary>
    /// 住院业务相关信息
    /// </summary>
    public class ResElseInfo
    {
        public string rela_hosp_id { get; set; }
        public string rela_serial_no { get; set; }
        public string serial_apply { get; set; }
        public string reg_flag { get; set; }
        public string biz_times { get; set; }
        public string declare_year { get; set; }
    }
}
