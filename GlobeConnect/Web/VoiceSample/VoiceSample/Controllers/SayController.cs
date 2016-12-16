using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;
using static Globe.Connect.Voice.Actions.Key;

namespace VoiceSample.Controllers
{
    public class SayController : Controller
    {
        public ActionResult Index()
        {
            Voice voice = new Voice();

			voice.Say("Welcome to my Tropo Web API.");
			voice.Say("I will play an audio file for you, please wait.");
			voice.Say(
				VALUE("http://openovate.com/tropo-rocks.mp3")
			);

			return Content(voice.Render().ToString(), "application/json");
        }
    }
}
