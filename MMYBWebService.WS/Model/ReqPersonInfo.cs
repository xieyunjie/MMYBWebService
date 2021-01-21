using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    public class ReqPersonInfo
    {
        public string idcard { get; set; }
        public string hospital_id { get; set; }
        public string biz_type { get; set; }
        public string center_id { get; set; }
    }
}
