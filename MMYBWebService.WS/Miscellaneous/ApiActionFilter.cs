 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MMYBWebService.WS.Miscellaneous
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ApiActionFilter : ActionFilterAttribute
    {
 
        private readonly NLog.Logger logger;
        private string bodyStr = "";

        public ApiActionFilter()
        {

            logger = NLog.LogManager.GetLogger(typeof(ApiActionFilter).ToString());
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            DateTime d1 = DateTime.Now;

            try
            {
                var isDefineAllowAnonymous = false;
                
                var controllerActionDescriptor = context.ActionDescriptor.ControllerDescriptor;

                //context.HttpContext.Request.Body.Position = 0;
                ////string bodyStr = string.Empty;
                ////using (var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                //using (var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8))
                //{
                //    var bodyRead = reader.ReadToEndAsync();
                //    bodyStr = bodyRead.Result;  //把body赋值给bodyStr
                //}
                logger.Info($" ApiActionFilter {context.ActionDescriptor.ActionName} 执行开始...");
                logger.Info($"Input: {bodyStr}");

                if (controllerActionDescriptor != null)
                {
                    isDefineAllowAnonymous = controllerActionDescriptor.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));

                    if (isDefineAllowAnonymous == false)
                    { 
                        isDefineAllowAnonymous = context.ActionDescriptor.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
                    }
                }
                base.OnActionExecuting(context);
                DateTime d2 = DateTime.Now;
                var ts = d2 - d1;
                if (ts.TotalSeconds >= 5)
                {
                    logger.Warn(" ApiActionFilter 访问时间过长：" + ts.TotalMilliseconds.ToString());
                }

            }
            catch (Exception ex)
            {
                context.Result = RetUtil.ExceptionErr(ex.Message);
                logger.Error(" ApiActionFilter 访问异常：" + ex.Message);
                logger.Info(" ApiActionFilter 访问异常：" + ex.Message);

            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Result is JsonResultEx)
            {
                var r = context.Result as JsonResultEx;
                logger.Info($"Output: {Newtonsoft.Json.JsonConvert.SerializeObject(r.Ret)}");
            }
            else if (context.Exception!=null)
            { 
                logger.Error(" ApiActionFilter 访问异常：" + bodyStr); 
                logger.Error(" ApiActionFilter 访问异常：" + context.Exception.StackTrace);
            }
            logger.Info(" ApiActionFilter 执行结束...");
        }
    }
}
