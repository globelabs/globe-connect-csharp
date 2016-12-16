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
    public class TransferController : Controller
    {
        public ActionResult Index()
        {
            Voice voice = new Voice();

			voice.Say("Welcome to my Tropo Web API, you are now being transferred.");

			Say e1 = new Say(
				VALUE("Sorry, I did not hear anything."),
				EVENT("timeout")
			);

			Say e2 = new Say(
				VALUE("Sorry, that was not a valid option."),
				EVENT("nomatch:1")
			);

			Say e3 = new Say(
				VALUE("Nope, still not a valid response"),
				EVENT("nomatch:2")
			);

			Say say = new Say(
				VALUE("Please enter your 5 digit zip code."),
				ARRAY(e1, e2, e3)
			);

			Choices choices = new Choices("[5 DIGITS]");

			Ask ask = new Ask(
				INSTANCE(choices),
				ATTEMPTS(3),
				BARGEIN(false),
				NAME("foo"),
				REQUIRED(true),
				INSTANCE(say),
				TIMEOUT(5)
			);

			On ring = new On(
				EVENT("ring"),
				INSTANCE(new Say("http://openovate.com/hold-music.mp3"))
			);

			On connect = new On(
			   EVENT("connect"),
			   INSTANCE(ask)
			);

			On on = new On(ARRAY(ring, connect));

			voice.Transfer(
				TO("9053801178"),
				RING_REPEAT(2),
				INSTANCE(on)
			);

			return Content(voice.Render().ToString(), "application/json");
        }
    }
}
