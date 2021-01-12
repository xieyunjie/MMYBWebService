using Microsoft.AspNetCore.Mvc;
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

        public IActionResult GetPersionInfo()
        {
            return RetData.Success();
        }

    }
}
