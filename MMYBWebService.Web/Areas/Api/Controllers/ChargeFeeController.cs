using Microsoft.AspNetCore.Mvc;
using MMYBWebService.Web.Model;
using MMYBWebService.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Areas.Api.Controllers
{
    public class ChargeFeeController : ApiBaseController
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Charge([FromBody] ReqChargeFee req)
        {
            ResChargeFee_DS data = InterfaceHNUtil.ChargeFee(req);

            return RetData.SuccessData(data);
        }

        public IActionResult Change([FromBody] ReqChangeFee req)
        {
            ResChargeFee_DS data = InterfaceHNUtil.ChangeFee(req);

            return RetData.SuccessData(data);
        }
    }
}
