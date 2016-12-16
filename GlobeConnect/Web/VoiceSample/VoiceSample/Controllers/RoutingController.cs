using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;
using static Globe.Connect.Voice.Actions.Key;

namespace VoiceSample.Controllers
{
    public class RoutingController : Controller
    {
        public ActionResult Index()
        {
			Voice voice = new Voice();

			voice.Say("Welcome to my Tropo Web API.");
			voice.On(
				EVENT("continue"),
				NEXT("/routing/route1")
			);

			return Content(voice.Render().ToString(), "application/json");
        }

		public ActionResult Route1()
		{
			Voice voice = new Voice();

			voice.Say("Hello from resource one!");
			voice.On(
				EVENT("continue"),
				NEXT("/routing/route2")
			);

			return Content(voice.Render().ToString(), "application/json");
		}

		public ActionResult Route2()
		{
			Voice voice = new Voice();

			voice.Say("Hello from resource two! thank you.");

			return Content(voice.Render().ToString(), "application/json");
		}
    }
}
