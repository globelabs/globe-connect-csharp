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
	public class TransferWhisperController : Controller
	{
		public ActionResult Index()
		{
			VoiceBase voice = new VoiceBase();

			voice.Say("Welcome to my Tropo Web API, please hold while you are being transferred.");

			Say say = new Say("Press 1 to accept this call or any other number to reject");

			Choices choices = new Choices(
				VALUE("1"),
				MODE(Mode.DTMF)
			);

			Ask ask = new Ask(
				INSTANCE(choices),
				NAME("color"),
				INSTANCE(say),
				TIMEOUT(60)
			);

			On connect1 = new On(
				EVENT("connect"),
				INSTANCE(ask)
			);

			On connect2 = new On(
				EVENT("connect"),
				INSTANCE(new Say("You are now being connected."))
			);

			On ring = new On(
				EVENT("ring"),
				INSTANCE(new Say("http://openovate.com/hold-music.mp3"))
			);

			On connect = new On(ARRAY(ring, connect1, connect2));

			voice.Transfer(
				TO("9054799241"),
				NAME("foo"),
				INSTANCE(connect),
				REQUIRED(true),
				TERMINATOR("*")
			);

			voice.On(
				EVENT("incomplete"),
				NEXT("/transferwhisper/hangup"),
				INSTANCE(new Say("You are now being disconnected."))
			);

			return Content(voice.Render().ToString(), "application/json");
		}

		public ActionResult Hangup()
		{
			VoiceBase voice = new VoiceBase();

			voice.Hangup();

			return Content(voice.Render().ToString(), "application/json");
		}
	}
}
