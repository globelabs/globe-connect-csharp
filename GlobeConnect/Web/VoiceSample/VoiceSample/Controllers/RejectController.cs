using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;

namespace VoiceSample.Controllers
{
    public class RejectController : Controller
    {
        public ActionResult Index()
        {
			Voice voice = new Voice();

			voice.Reject();

			return Content(voice.Render().ToString(), "application/json");
        }
    }
}
