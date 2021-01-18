﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Areas.Api.Controllers
{
    public class TestController : ApiBaseController
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

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

        [HttpPost]
        public IActionResult GetModelJson(string modelClass)
        {
            try
            {
                Type type = Type.GetType(modelClass);
                var obj = System.Activator.CreateInstance(type);

                return RetData.SuccessData(obj);
            }
            catch (Exception ex)
            {
                return RetData.ExceptionErr(ex.Message);
            }
        }
    }
}
