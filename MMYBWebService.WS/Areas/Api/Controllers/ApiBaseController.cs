using MMYBWebService.WS.Miscellaneous;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMYBWebService.WS.Areas.Api.Controllers
{
    [ApiActionFilter]
    public abstract class ApiBaseController : Controller
    {
        // GET: Api/ApiBase
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public T GetReqObj<T>(Stream stream) where T : new()
        {
            string json = GetRequestData(stream, out string fallback);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public string GetRequestData(Stream stream, out string fncallback)
        {
            fncallback = null;
            string postString = "";
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            if (stream.CanRead)
            {
                using (StreamReader sr = new StreamReader(stream))
                {

                    postString = sr.ReadToEnd();
                    if (postString.Contains("&"))
                    {
                        string[] postStrings = postString.Split('&');
                        postString = postStrings[0];
                        for (int i = 0; i < postStrings.Length; i++)
                        {
                            if (postStrings[i].IndexOf("callbackparam") > -1)
                            {
                                string[] s1 = postStrings[1].Split('=');
                                if (s1.Length > 1)
                                    fncallback = s1[1];
                                else
                                    fncallback = null;
                                break;
                            }
                        }
                        // [3] = "callbackparam=fncallbackappointend"

                    }
                    //else
                    //{
                    //    fncallback = null;
                    //}
                }
                //Byte[] postBytes = new Byte[stream.Length];
                //stream.Read(postBytes, 0, (Int32)stream.Length);
                //postString = Encoding.UTF8.GetString(postBytes);
            }
            //else
            //    fncallback = null;

            return postString;

        }
    }
}