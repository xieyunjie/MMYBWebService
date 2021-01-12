﻿using Microsoft.AspNetCore.Mvc;
using MMYBWebService.Web.Model;
using MMYBWebService.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Areas.Api.Controllers
{
    public class PersonController : ApiBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPersionInfo([FromBody] ReqPersonInfo req)
        {
            ResPersonInfo_DS data = InterfaceHNUtil.GetPersonInfo(req);

            return RetData.SuccessData(data);
        }

        public IActionResult TestInfo()
        {
            return RetData.Success();
        }

    }
}
