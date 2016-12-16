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
    public class ConferenceController : Controller
    {
        public ActionResult Index()
        {
			Voice voice = new Voice();

			voice.Say("Welcome to my Tropo Web API Conference Call.");

			voice.Conference(
				ID("12345"),
				MUTE(false),
				NAME("foo"),
				PLAY_TONES(true),
				TERMINATOR("#"),
				INSTANCE(new JoinPrompt(
					VALUE("http://openovate.com/hold-music.mp3")
				)),
				INSTANCE(new LeavePrompt(
					VALUE("http://openovate.com/hold-music.mp3")
				))
			);

			return Content(voice.Render().ToString(), "application/json");
        }
    }
}
