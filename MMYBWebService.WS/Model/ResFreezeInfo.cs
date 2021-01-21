using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    /// <summary>
    /// 个人基金冻结信息
    /// </summary>
    public class ResFreezeInfo
    {
        public string fund_id { get; set; }
        public string fund_name { get; set; }
        public string indi_freeze_status { get; set; }
    }
}
