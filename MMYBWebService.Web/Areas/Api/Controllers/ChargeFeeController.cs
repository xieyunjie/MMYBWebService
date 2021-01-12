using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Areas.Api.Controllers
{
    public class ChargeFeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
