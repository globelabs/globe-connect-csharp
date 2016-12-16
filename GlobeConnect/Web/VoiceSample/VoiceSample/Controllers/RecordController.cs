using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using Globe.Connect.Voice.Enums;
using static Globe.Connect.Voice.Actions.Key;

using VoiceBase = Globe.Connect.Voice.Voice;

namespace VoiceSample.Controllers
{
    public class RecordController : Controller
    {
        public ActionResult Index()
        {
			VoiceBase voice = new VoiceBase();

			voice.Say("Welcome to my Tropo Web API.");

			Say timeout = new Say(
				VALUE("Sorry, I did not hear anything. Please call back."),
				EVENT("timeout")
			);

			Say say = new Say(VALUE("Please leave a message"), ARRAY(timeout));

			Choices choices = new Choices(TERMINATOR("#"));

			Transcription transcription = new Transcription(
				ID("1234"),
				URL("mailto:charles.andacc@gmail.com")
			);

			voice.Record(
				ATTEMPTS(3),
				BARGEIN(false),
				METHOD("POST"),
				REQUIRED(true),
				INSTANCE(say),
				NAME("foo"),
				URL("http://openovate.com/globe.php"),
				FORMAT(Format.WAV),
				INSTANCE(choices),
				INSTANCE(transcription)
			);

            return Content(voice.Render().ToString(), "application/json");
        }
    }
}
