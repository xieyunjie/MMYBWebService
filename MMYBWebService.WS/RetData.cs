
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MMYBWebService.WS
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
    }

    public class JsonResultEx : JsonResult
    {
        private RetData _ret;
        public JsonResultEx(RetData ret)
        {
            _ret = ret;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_ret);
            response.Write(json);
        }
    }

    public class RetUtil
    {

        public static JsonResult Success(string inputMsg = null)
        {

            var r = new RetData()
            {
                data = null,
                msg = inputMsg ?? "操作成功"
            };

            return new JsonResultEx(r);
        }
        public static JsonResult SuccessData(object data, int page = 0, int size = 0, int total = 0)
        {
            var r = new RetData()
            {
                data = data,
                page = page,
                size = size,
                total = total
            };

            return new JsonResultEx(r);
        }
        public static JsonResult Failure(string msg = "操作失败")
        {

            var r = new RetData()
            {
                data = null,
                success = false,
                msg = msg
            };

            return new JsonResultEx(r);
        }
        public static JsonResult Forbidden()
        {
            var r = new RetData()
            {
                code = (int)HttpStatusCode.Forbidden,
                msg = "系统提示- 无权限操作",
                success = false
            };

            return new JsonResultEx(r);
        }
        public static JsonResult ExceptionErr(string inputMsg = null)
        {
            var r = new RetData()
            {
                code = (int)HttpStatusCode.InternalServerError,
                msg = inputMsg ?? "系统提示 - 系统错误",
                success = false
            };

            return new JsonResultEx(r);
        }
    }
}
