using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.WS.Model
{
    public class ResChargeFee_DS
    {
        public List<ResBizInfo> BizInfoList { get; set; }
        public List<ResPayInfo> PayInfoList { get; set; }
        public List<ResDetailPay> DetailPayList { get; set; }
    }
}
