using Microsoft.AspNetCore.Mvc;
using MMYBWebService.Web.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("Api/[controller]/[action]")]
    [TypeFilter(typeof(ApiActionFilter))]
    public abstract class ApiBaseController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
