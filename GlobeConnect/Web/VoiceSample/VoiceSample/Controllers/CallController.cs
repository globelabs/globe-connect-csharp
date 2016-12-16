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
    public class CallController : Controller
    {
        public ActionResult Index()
        {
            Voice voice = new Voice();

			voice.Call(
				TO("sip:9065272450@tropo.net"),
				FROM("9065272450")
			);

			voice.Say(ARRAY(new Say("Hello World")));

			return Content(voice.Render().ToString(), "application/json");
        }
    }
}
