using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Miscellaneous
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ApiActionFilter : ActionFilterAttribute
    {
        private readonly Logger<ApiActionFilter> logger;
        private string bodyStr = "";

        public ApiActionFilter(Logger<ApiActionFilter> log)
        {
            logger = log;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            DateTime d1 = DateTime.Now;

            try
            {
                var isDefineAllowAnonymous = false;
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                context.HttpContext.Request.Body.Position = 0;
                //string bodyStr = string.Empty;
                //using (var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                using (var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8))
                {
                    var bodyRead = reader.ReadToEndAsync();
                    bodyStr = bodyRead.Result;  //把body赋值给bodyStr
                }
                logger.LogInformation($" ApiActionFilter {controllerActionDescriptor.DisplayName} 执行开始...");
                logger.LogInformation($"Input: {bodyStr}");

                if (controllerActionDescriptor != null)
                {
                    isDefineAllowAnonymous = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));

                    if (isDefineAllowAnonymous == false)
                    {
                        isDefineAllowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
                    }
                }
                base.OnActionExecuting(context);
                DateTime d2 = DateTime.Now;
                var ts = d2 - d1;
                if (ts.TotalSeconds >= 5)
                {
                    logger.LogWarning(" ApiActionFilter 访问时间过长：" + ts.TotalMilliseconds.ToString());
                }

            }
            catch (Exception ex)
            {
                context.Result = RetData.ExceptionErr(ex.Message);
                logger.LogError(" ApiActionFilter 访问异常：" + ex.Message);
                logger.LogInformation(" ApiActionFilter 访问异常：" + ex.Message);

            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Result is OkObjectResult)
            {
                var r = context.Result as OkObjectResult;
                logger.LogInformation($"Output: {Newtonsoft.Json.JsonConvert.SerializeObject(r.Value)}");
            }
            else if (context.Exception!=null)
            { 
                logger.LogError(" ApiActionFilter 访问异常：" + bodyStr); 
                logger.LogError(" ApiActionFilter 访问异常：" + context.Exception.StackTrace);
            }
            logger.LogInformation(" ApiActionFilter 执行结束...");
        }
    }
}
