using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

using Newtonsoft.Json.Linq;

namespace VoiceSample.Controllers
{
    public class AskAnswerController : Controller
    {
        public ActionResult Index()
		{
			return Content("{}", "application/json");
        }

		public ActionResult Ask()
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
				NEXT("/askanswer/answer"),
				REQUIRED(true)
			);

			return Content(voice.Render().ToString(), "application/json");
		}

		public ActionResult Answer()
		{
			Voice voice = new Voice();

			String data = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
			JObject result = new Result(JObject.Parse(data)).GetResult();

			voice.Say("Your zip code is " + result.GetValue("interpretation") + ", thank you!");

			return Content(voice.Render().ToString(), "application/json");
		}
    }
}
