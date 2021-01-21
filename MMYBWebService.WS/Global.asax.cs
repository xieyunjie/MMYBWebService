using MMYBWebService.WS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MMYBWebService.WS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InterfaceHNSetting config = new InterfaceHNSetting()
            {

                CenterId = "440902",
                CenterName = "茂名市医保中心",
                HospitalId = "40999179",
                HospitalName = "大参林",
                HospitalId_pwd = "",
                Server = "frp.sq580.com",
                Port = 20105,
                Servle = "sicp3_test/ProcessAll",
                StaffId = "440902088",
                StaffId_pwd = "440902088",
            };

            InterfaceHNUtil.config = config;
        }
    }
}
