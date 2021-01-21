 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MMYBWebService.WS.Areas.Api.Controllers
{
    public class TestController : ApiBaseController
    {
        //public JsonResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult TestInfo()
        {
            return RetUtil.Success();
        }
        [HttpPost]
        public JsonResult TestExceptionInfo()
        {
            throw new Exception("系统错误！");
        }

        [HttpPost]
        public JsonResult TestComInfo()
        {
            Type t = Type.GetTypeFromCLSID(new Guid("F0E70DF1-66B0-40c2-8210-40CEBBB8A6DA"));

            object obj = System.Activator.CreateInstance(t);

            object r = t.InvokeMember("test", System.Reflection.BindingFlags.InvokeMethod, null, obj, null);

            return RetUtil.SuccessData(r);
        }

        [HttpPost]
        public JsonResult GetModelJson(string modelClass)
        {
            try
            {
                Type type = Type.GetType(modelClass);
                var obj = System.Activator.CreateInstance(type);

                return RetUtil.SuccessData(obj);
            }
            catch (Exception ex)
            {
                return RetUtil.ExceptionErr(ex.Message);
            }
        }
    }
}
