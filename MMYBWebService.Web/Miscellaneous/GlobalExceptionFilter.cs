
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Miscellaneous
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _env;
        private readonly Logger<GlobalExceptionFilter> logger;
        public GlobalExceptionFilter(IWebHostEnvironment env, Logger<GlobalExceptionFilter> log)
        {
            _env = env;
            logger = log;
        }
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                logger.LogError(context.HttpContext.Request.GetDisplayUrl() + " 执行异常...\r\n" + context.Exception.StackTrace);

                if (_env.IsDevelopment() || _env.IsStaging())
                {

                    if (context.Exception.InnerException == null)
                    {
                        context.Result = RetData.ExceptionErr(context.Exception.Message);
                    }
                    else
                    {
                        context.Result = RetData.ExceptionErr(context.Exception.InnerException.GetBaseException().Message);
                    }
                }
                else
                {
                    context.Result = RetData.ExceptionErr("系统繁忙");
                }
                context.ExceptionHandled = true;
            }
        }
    }
}
