using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;
using static Globe.Connect.Voice.Actions.Key;

namespace VoiceSample.Controllers
{
    public class WaitController : Controller
    {
        public ActionResult Index()
        {
			Voice voice = new Voice();

			voice.Say("Welcome to my Tropo Web API, please wait for a while.");
			voice.Wait(MILLISECONDS(5000), ALLOW_SIGNALS(true));
			voice.Say("Thank you for waiting!");

            return Content(voice.Render().ToString(), "application/json");
        }
    }
}
