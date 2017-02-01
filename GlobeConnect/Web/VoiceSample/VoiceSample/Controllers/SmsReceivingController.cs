using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json.Linq;

namespace VoiceSample.Controllers
{
    public class SmsReceivingController : Controller
    {
        public ActionResult Index()
        {
			String data = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
			JObject result = JObject.Parse(data);

			Console.WriteLine(result);

			return Content(result.ToString(), "application/json");
        }
    }
}
