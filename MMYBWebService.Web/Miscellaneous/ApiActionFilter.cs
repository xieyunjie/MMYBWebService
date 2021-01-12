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

        public ApiActionFilter(Logger<ApiActionFilter> log)
        {
            logger = log;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            DateTime d1 = DateTime.Now;

            //try
            //{
            var isDefineAllowAnonymous = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            context.HttpContext.Request.Body.Position = 0;
            string bodyStr = string.Empty;
            using (var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
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

            //bool bl = this.CheckUserOrg(context, out UserOrgConfig userOrgConfig);

            //logModel = new OuRecipeOrderListLog()
            //{
            //    UserName = "",
            //    UserCode = userOrgConfig.UserCode,
            //    OrgCode = userOrgConfig.OrgCode,
            //    OperTime = d1,
            //    Logger = "",
            //    Level = "",
            //    Method = controllerActionDescriptor.MethodInfo.Name,
            //    ReqUrl = $"{context.HttpContext.Request.Host.Value}{context.HttpContext.Request.Path.Value}",
            //    ReqData = bodyStr,
            //    ResData = "",
            //    Status = 0,
            //    Message = ""
            //};


            //if (isDefineAllowAnonymous == true)   // 允许匿名访问
            //{
            //    logModel.Message = " ApiActionFilter 匿名访问...";
            //    logger.LogInformation(" ApiActionFilter 匿名访问...");
            //    base.OnActionExecuting(context);
            //    return;
            //}
            //else if (bl == false)
            //{
            //    logModel.Status = -1;
            //    logModel.Message = $" ApiActionFilter 没访问权限， userCode：{userOrgConfig.UserCode} orgCode：{userOrgConfig.OrgCode}";

            //    logger.LogError($" ApiActionFilter 没访问权限， userCode：{userOrgConfig.UserCode} orgCode：{userOrgConfig.OrgCode}");
            //    context.Result = Common.RetData.Forbidden();
            //}
            //else
            //{

            //logger.LogInformation($" ApiActionFilter  userCode：{userOrgConfig.UserCode} orgCode：{userOrgConfig.OrgCode}");
            base.OnActionExecuting(context);
            DateTime d2 = DateTime.Now;
            var ts = d2 - d1;
            if (ts.TotalSeconds >= 5)
            {
                logger.LogWarning(" ApiActionFilter 访问时间过长：" + ts.TotalMilliseconds.ToString());
            }

            //    }

            //}
            //catch (Exception ex)
            //{
            //    context.Result = Common.RetData.ExceptionErr(ex.Message);
            //}
            //finally
            //{
            //    if (logModel != null)
            //    {
            //        long l = logDao.Add(logModel);
            //        logModel.ID = Convert.ToInt32(l);
            //    }
            //}
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Result is OkObjectResult)
            {
                var r = context.Result as OkObjectResult;
                logger.LogInformation($"Output: {Newtonsoft.Json.JsonConvert.SerializeObject(r.Value)}");
            }


            //try
            //{
            //    if (logModel != null)
            //    {
            //        if (context.Exception != null)
            //        {
            //            logModel.Status = -1;
            //            logModel.Message = context.Exception.Message;
            //        }
            //        else
            //        {
            //            logModel.Status = 1;
            //            if (context.Result is OkObjectResult)
            //            {
            //                var r = context.Result as OkObjectResult;
            //                logModel.ResData = Newtonsoft.Json.JsonConvert.SerializeObject(r.Value);
            //            }
            //        }
            //        logDao.Update(logModel);
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            logger.LogInformation(" ApiActionFilter 执行结束...");
        }
    }
}
