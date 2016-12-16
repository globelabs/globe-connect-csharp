using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;

namespace VoiceSample.Controllers
{
    public class HangupController : Controller
    {
        public ActionResult Index()
        {
			Voice voice = new Voice();

			voice.Say("Welcome to my Tropo Web API, thank you!");
			voice.Hangup();

            return Content(voice.Render().ToString(), "application/json");
        }
    }
}
