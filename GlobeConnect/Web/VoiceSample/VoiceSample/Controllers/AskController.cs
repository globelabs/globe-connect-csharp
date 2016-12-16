using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

namespace VoiceSample.Controllers
{
    public class AskController : Controller
    {
        public ActionResult Index()
        {
			Voice voice = new Voice();

			voice.Say("Welcome to my Tropo Web API.");

			Say say = new Say("Please enter your 5 digit zip code.");
			Choices choices = new Choices("[5 DIGITS]");

			voice.Ask(
				INSTANCE(choices),
				ATTEMPTS(3),
				BARGEIN(false),
				NAME("foo"),
				REQUIRED(true),
				INSTANCE(say),
				TIMEOUT(10)
			);

			voice.On(
				EVENT("continue"),
				NEXT("http://somefakehost.com:8000/"),
				REQUIRED(true)
			);

			return Content(voice.Render().ToString(), "application/json");
        }
    }
}
