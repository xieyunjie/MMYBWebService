using Microsoft.AspNetCore.Mvc;
using MMYBWebService.Web.Model;
using MMYBWebService.Web.Model.Validator;
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
        [HttpPost]
        public IActionResult Charge([FromBody] ReqChargeFee req)
        {
            var validator = new ReqChargeFeeValidator();
            string validInfo = CommonUtil.Validate(req, validator);

            ResChargeFee_DS data = InterfaceHNUtil.ChargeFee(req);

            return RetData.SuccessData(data);
        }

        [HttpPost]
        public IActionResult Change([FromBody] ReqChangeFee req)
        {
            var validator = new ReqChangeFeeValidator();
            string validInfo = CommonUtil.Validate(req, validator);

            ResChargeFee_DS data = InterfaceHNUtil.ChangeFee(req);

            return RetData.SuccessData(data);
        }
    }
}
