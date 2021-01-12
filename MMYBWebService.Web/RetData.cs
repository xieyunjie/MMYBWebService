using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MMYBWebService.Web
{
    public class RetData
    {
        public int code { get; set; } = 200;
        public bool success { get; set; } = true;
        public object data { get; set; }
        public string msg { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public int total { get; set; }

        public static IActionResult Success(string inputMsg = null)
        {

            var r = new RetData()
            {
                data = null,
                msg = inputMsg ?? "操作成功"
            };

            return new OkObjectResult(r);
        }
        public static IActionResult SuccessData(object data, int page = 0, int size = 0, int total = 0)
        {
            var r = new RetData()
            {
                data = data,
                page = page,
                size = size,
                total = total
            };

            return new OkObjectResult(r);
        }

        public static IActionResult Failure(string msg = "操作失败")
        {

            var r = new RetData()
            {
                data = null,
                success = false,
                msg = msg
            };

            return new OkObjectResult(r);
        }

        public static IActionResult Forbidden()
        {
            var r = new RetData()
            {
                code = (int)HttpStatusCode.Forbidden,
                msg = "系统提示- 无权限操作",
                success = false
            };

            return new OkObjectResult(r);
        }

        public static IActionResult ExceptionErr(string inputMsg = null)
        {
            var r = new RetData()
            {
                code = (int)HttpStatusCode.InternalServerError,
                msg = inputMsg ?? "系统提示 - 系统错误",
                success = false
            };

            return new OkObjectResult(r);
        }
    }
}
