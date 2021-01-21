
using MMYBWebService.WS.Model;
using MMYBWebService.WS.Model.Validator;
using MMYBWebService.WS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MMYBWebService.WS.Areas.Api.Controllers
{
    public class PersonController : ApiBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetPersonInfo()
        {
            try
            {

                ReqPersonInfo req = GetReqObj<ReqPersonInfo>(this.HttpContext.Request.InputStream);
                var validator = new ReqPersonInfoValidator();
                string validInfo = CommonUtil.Validate(req, validator);

                ResPersonInfo_DS data = InterfaceHNUtil.GetPersonInfo(req);

                return RetUtil.SuccessData(data);
            }
            catch (Exception ex)
            {
                return RetUtil.ExceptionErr(ex.Message);
            }

        }

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

    }
}
