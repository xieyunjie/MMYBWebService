using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model
{
    public class ResPersonInfo_DS
    {
        public List<ResPersonInfo> PersonInfoList { get; set; } = new List<ResPersonInfo>();
        public List<ResSpInfo> SpInfoList { get; set; } = new List<ResSpInfo>();
        public List<ResElseInfo> ElseInfoList { get; set; } = new List<ResElseInfo>();
        public List<ResLastBizInfo> LastBizInfoList { get; set; } = new List<ResLastBizInfo>();
        public List<ResFreezeInfo> FreezeInfoList { get; set; } = new List<ResFreezeInfo>();
        public List<ResTotalBizInfo> TotalBizInfoList { get; set; } = new List<ResTotalBizInfo>();
    }
}
