﻿using Microsoft.AspNetCore.Mvc;
using MMYBWebService.Web.Model;
using MMYBWebService.Web.Model.Validator;
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

        [HttpPost]
        public IActionResult GetPersonInfo([FromBody] ReqPersonInfo req)
        {
            var validator = new ReqPersonInfoValidator();
            string validInfo = CommonUtil.Validate(req, validator);  

            ResPersonInfo_DS data = InterfaceHNUtil.GetPersonInfo(req);

            return RetData.SuccessData(data);
        }

        [HttpPost]
        public IActionResult TestInfo()
        {
            return RetData.Success();
        }
        [HttpPost]
        public IActionResult TestExceptionInfo()
        {
            throw new Exception("系统错误！");
        }

    }
}
