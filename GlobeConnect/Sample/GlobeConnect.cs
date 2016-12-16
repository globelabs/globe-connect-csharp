using System;
using System.Net;

using Globe.Connect;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

using Globe.Connect.Voice.Enums;
using VoiceEnum = Globe.Connect.Voice.Enums.Voice;
using VoiceBase = Globe.Connect.Voice.Voice;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sample
{
	public class GlobeConnect
	{
		const string SHORT_CODE = "21584130";
		const string APP_ID = "5ozgSgeRyeHzacXo55TR65HnqoAESbAz";
		const string APP_SECRET = "3dbcd598f268268e13550c87134f8de0ec4ac1100cf0a68a2936d07fc9e2459e";
		const string ACCESS_TOKEN = "JO3SpcC-AFiC461wgOxUPDmsOTc5YiMayoK1GnQcduc";

		static void Main(String[] args)
		{
			// test http request
			TestHttpRequest();

			// test authentication
			//TestAuthentication();

			//test sms
			//TestSms();

			// test binary sms
			//TestBinarySms();

			// test location
			//TestLocation();

			// test payment
			//TestPayment();

			// test subscriber
			//TestSubscriber();

			// test ussd
			//TestUssd();

			// test amax
			//TestAmax();

			// test voice
			TestVoice();
		}

		static void TestHttpRequest()
		{
			HttpRequest request = new HttpRequest();

			string url = "http://www.mocky.io/v2/5185415ba171ea3a00704eed";

			Console.WriteLine("Sending GET request to: " + url);

			HttpResponse getResponse = request
				.SetUrl(url)
				.SendGet();

			Console.WriteLine("Response: -->");
			Console.WriteLine(getResponse.GetDynamicResponse());

			Console.WriteLine("Send POST request to: " + url);

			var data = new {
				Hello = "World"
			};

			HttpResponse postResponse = request
				.SetUrl(url)
				.SetData(data)
				.SendPost();

			Console.WriteLine("Response: -->");
			Console.WriteLine(postResponse);
			Console.WriteLine(postResponse.GetDynamicResponse());
		}

		static void TestAuthentication()
		{
			Authentication auth = new Authentication(APP_ID, APP_SECRET);

			Console.WriteLine("Get dialog url:");
			Console.WriteLine(auth.GetDialogUrl());
			
			string code = "G4HBMexKfaM9E7SG4LpkHRBoLGf9Go6qSnBno8HRKXnes7doqEukgq4bCq59nKfR7KX6Uorknysa8EXyHoxEaRhzGo57tLn4gduLkaE7S9ke9RtpBjgauaeRKpu4RcoX6y4cRaxuGzjkKuyzedXtkra8qSbe47LueyonxtgoEorhpkEoaHLkkResXyKR4U4K996f4EqB7CRLoKGuBjXorsAxnrpH9poqrSAEo6ef7XLGXHyK9R9SLregxfaM6XxH";
			
			Console.WriteLine("Get access token:");
			Console.WriteLine(auth.GetAccessToken(code).GetDynamicResponse());
		}

		static void TestSms()
		{
			Sms sms = new Sms(SHORT_CODE, ACCESS_TOKEN);

			Console.WriteLine("Sending SMS:");

			dynamic response = sms
				.SetReceiverAddress("+639065272450")
				.SetMessage("Hello World from C#")
				.SendMessage()
				.GetDynamicResponse();

			Console.WriteLine("Response: -->");
			Console.WriteLine(response);
		}

		static void TestBinarySms()
		{
			BinarySms sms = new BinarySms(SHORT_CODE, ACCESS_TOKEN);

			Console.WriteLine("Sending Binary SMS:");

			dynamic response = sms
				.SetReceiverAddress("9065272450")
				.SetUserDataHeader("06050423F423F4")
				.SetDataCodingScheme(2)
				.SetBinaryMessage("samplebinarymessage")
				.SendBinaryMessage()
				.GetDynamicResponse();

			Console.WriteLine("Response: -->");
			Console.WriteLine(response);
		}

		static void TestLocation()
		{
			Location location = new Location(ACCESS_TOKEN);

			Console.WriteLine("Get Location request:");

			dynamic response = location
				.SetAddress("+639065272450")
				.SetRequestedAccuracy(10)
				.GetLocation();

			Console.WriteLine("Response: -->");
			Console.WriteLine(response);
		}

		static void TestPayment()
		{
			Payment payment = new Payment(APP_ID, APP_SECRET, ACCESS_TOKEN);

			Console.WriteLine("Sending payment request:");

			dynamic response = payment
				.SetAmount(0.00)
				.SetDescription("my description")
				.SetEndUserId("9065272450")
				.SetReferenceCode("41301000202")
				.SetTransactionOperationStatus("Charged")
				.SendPaymentRequest()
				.GetDynamicResponse();

			Console.WriteLine("Response: -->");
			Console.WriteLine(response);

			Console.WriteLine("Get last reference code request:");

			response = payment
				.GetLastReferenceCode()
				.GetDynamicResponse();

			Console.WriteLine("Response: --> ");
			Console.WriteLine(response);
		}

		static void TestSubscriber()
		{
			Subscriber subscriber = new Subscriber(ACCESS_TOKEN);

			Console.WriteLine("Get subscriber balance request:");

			dynamic response = subscriber
				.SetAddress("+639065272450")
				.GetSubscriberBalance()
				.GetDynamicResponse();

			Console.WriteLine("Response: -->");
			Console.WriteLine(response);

			Console.WriteLine("Get subscriber reload amount request:");

			response = subscriber
				.SetAddress("+9065272450")
				.GetSubscriberReloadAmount()
				.GetDynamicResponse();

			Console.WriteLine("Response: -->");
			Console.WriteLine(response);
		}

		static void TestUssd()
		{
			Ussd ussd = new Ussd(ACCESS_TOKEN);

			Console.WriteLine("Send ussd request:");

			dynamic response = ussd
				.SetAddress("9065272450")
				.SetSenderAddress(SHORT_CODE)
				.SetUssdMessage("Simple USSD \n1: Hello \n2: Hello")
				.SetFlash(false)
				.SendUssdRequest()
				.GetDynamicResponse();

			Console.WriteLine("Response: -->");

			string sessionId = response["outboundUSSDMessageRequest"]["receiptRequest"]["sessionID"];

			Console.WriteLine("Session ID:" + sessionId);
			Console.WriteLine(response);

			Console.WriteLine("Reply ussd request:");

			try {
				response = ussd
					.SetAddress("9065272450")
					.SetSessionId(sessionId)
					.SetSenderAddress(SHORT_CODE)
					.SetUssdMessage("Simple USSD \n1: Foo \n1: Foo")
					.SetFlash(false)
					.ReplyUssdRequest()
					.GetDynamicResponse();

				Console.WriteLine("Response: -->");
				Console.WriteLine(response);
		
			} catch(WebException e) {
				Console.WriteLine(new System.IO.StreamReader(e.Response.GetResponseStream()).ReadToEnd());
			}
		}
	
		static void TestAmax()
		{
			Amax amax = new Amax(APP_ID, APP_SECRET);

			Console.WriteLine("Send reward request:");

			try {
				dynamic response = amax
					.SetAddress("9065272450")
					.SetRewardsToken("token")
					.SetPromo("LOAD 50")
					.SendRewardRequest()
					.GetDynamicResponse();

				Console.WriteLine("Response: -->");
				Console.WriteLine(response);
			} catch(WebException e) {
				Console.WriteLine(new System.IO.StreamReader(e.Response.GetResponseStream()).ReadToEnd());
			}
		}

		static void TestVoice() 
		{
			Header headers = new Header(
				new string[] {"x-sbc-from", "\"username\"<sip:00001234567@192.168.0.101>;tag=2a648c6e"},
				new string[] {"x-sbc-allow", "BYE"},
				new string[] {"x-sbc-user-agent", "sipgw-1.0"},
				new string[] {"x-sbc-contact", "<sip:00001234567@192.168.0.101:16000>"},
				new string[] {"Content-Length", "247"},
				new string[] {"To", "<sip:9991234567@10.6.60.100:5060>"},
				new string[] {"Contact", "<sip:username@10.6.60.100:5060>"},
				new string[] {"x-sbc-request-uri", "sip:990009369991234567@66.190.50.10:5060"},
				new string[] {"x-sbc-call-id", "OWE0OGFkMTE1ZGY4NTI1MmUzMjc1M2Y3Y2ExMzc2YhG."},
				new string[] {"x-sid", "39f4688b8896f024f3a3aebd0cfb40b2"},
				new string[] {"x-sbc-cseq", "1 INVITE"},
				new string[] {"x-sbc-max-forwards", "70"},
				new string[] {"CSeq", "2 INVITE"},
				new string[] {"Via", "SIP/2.0/UDP 66.190.50.10:5060;received=10.6.60.100"},
				new string[] {"x-sbc-record-route", "<sip:190.40.250.230:5061;r2=on;lr;ftag=2a648c6e>"},
				new string[] {"Call-ID", "0-13c4-4b7d8ff7-1c3c1b82-7935-1d10b081"},
				new string[] {"Content-Type", "application/sdp"},
				new string[] {"x-sbc-to", "<sip:990009369991427645@60.190.50.10:5060>"},
				new string[] {"From", "<sip:username@10.6.60.100:5060>;tag=0-13c4-4b7d8ff7-1c3c1b82-5c7b"}
			);

			Ask ask = new Ask(
				INSTANCE(
					new Choices(
						VALUE("1"),
						MODE(Mode.DTMF),
						TERMINATOR("*")
					)
				),
				ATTEMPTS(10),
				BARGEIN(true),
				MIN_CONFIDENCE(10),
				NAME("foo"),
				RECOGNIZER(Recognizer.US_ENGLISH),
				REQUIRED(false),
				INSTANCE(
					new Say("Hello World")
				),
				TIMEOUT(10),
				VOICE(VoiceEnum.VICTOR),
				INTER_DIGIT_TIMEOUT(10),
				SENSITIVITY(10),
				SPEECH_COMPLETE_TIMEOUT(10),
				SPEECH_INCOMPLETE_TIMEOUT(10)
			);

			Print(ask.GetAction());

			Call call = new Call(
				TO("+123456"),
				ANSWER_ON_MEDIA(false),
				CHANNEL(Channel.VOICE),
				FROM("+123456"),
				INSTANCE(headers),
				NAME("call"),
				NETWORK(Network.SIP),
				INSTANCE(
					new Record(
						INSTANCE(new Say(VALUE("Hello World"), ARRAY(new Say("Hello World"))))
					)
				),
				REQUIRED(true),
				TIMEOUT(10),
				ALLOW_SIGNALS(true),
				INSTANCE(
					new MachineDetection(
						INTRODUCTION("Hello World"),
						VOICE(VoiceEnum.VICTOR)
					)
				)
			);

			Print(call.GetAction());

			Choices choices = new Choices(
				VALUE("[5 DIGITS]"),
				MODE(Mode.DTMF),
				TERMINATOR("#")
			);

			Print(choices.GetAction());

			Conference conference = new Conference(
				ID("12345"),
				MUTE(false),
				NAME("conference"),
				PLAY_TONES(false),
				REQUIRED(false),
				TERMINATOR("#"),
				ALLOW_SIGNALS(false),
				INTER_DIGIT_TIMEOUT(10),
				INSTANCE(new JoinPrompt(VALUE("Please welcome the monster to the party!"))),
				INSTANCE(new LeavePrompt(VALUE("The monster has decide to depart!")))
			);

			Print(conference.GetAction());

			Hangup hangup = new Hangup();

			Print(hangup.GetAction());

			MachineDetection md = new MachineDetection(
				INTRODUCTION("Hello World"),
				VOICE(VoiceEnum.VICTOR)
			);

			Print(md.GetAction());

			Message message = new Message(
				INSTANCE(new Say("Hello World")),
				TO("+63525544787"),
				ANSWER_ON_MEDIA(false),
				CHANNEL(Channel.TEXT),
				FROM("2542562"),
				NAME("message"),
				NETWORK(Network.JABBER),
				REQUIRED(false),
				TIMEOUT(60),
				VOICE(VoiceEnum.CARMEN)
			);

			Print(message.GetAction());

			On on = new On(
				EVENT("continue"),
				NAME("event"),
				NEXT("http://openovate.com/hello.php"),
				REQUIRED(false),
				INSTANCE(new Say("Hello World")),
				INSTANCE(ask),
				INSTANCE(message),
				INSTANCE(new Wait(MILLISECONDS(3000), ALLOW_SIGNALS(true)))
			);

			Print(on.GetAction());

			Record record = new Record(
				ATTEMPTS(10),
				BARGEIN(false),
				BEEP(false),
				INSTANCE(choices),
				FORMAT(Format.MP3),
				MAX_SILENCE(10),
				MAX_TIME(60),
				METHOD("POST"),
				MIN_CONFIDENCE(10),
				NAME("recording"),
				REQUIRED(false),
				INSTANCE(new Say("Hello World")),
				TIMEOUT(10),
				INSTANCE(
					new Transcription(
						ID("12345"), 
						URL("https//gmail.com"), 
						EMAIL_FORMAT("format")
					)
				),
				URL("http://openovate.com/recording.js"),
				USERNAME("admin"),
				PASSWORD("admin"),
				VOICE(VoiceEnum.CARLOS),
				ALLOW_SIGNALS(false),
				INTER_DIGIT_TIMEOUT(10)
			);

			Print(record.GetAction());

			Redirect redirect = new Redirect(TO("+12345"), NAME("charles"), REQUIRED(false));

			Print(redirect.GetAction());

			Reject reject = new Reject();

			Print(reject.GetAction());

			Say say = new Say(
				VALUE("Hello World"),
				AS(As.DIGITS),
				NAME("say"),
				REQUIRED(false),
				VOICE(VoiceEnum.BERNARD),
				ALLOW_SIGNALS(false)
			);

			Print(say.GetAction());
		
			StartRecording sr = new StartRecording(
				FORMAT(Format.MP3),
				METHOD("POST"),
				URL("http://openovate.com/recording.js"),
				USERNAME("admin"),
				PASSWORD("admin"),
				TRANSCRIPTION_ID("12345"),
				TRANSCRIPTION_EMAIL_FORMAT("format"),
				TRANSCRIPTION_OUT_URI("http://openovate.com/recording.js")
			);

			Print(sr.GetAction());

			Transfer transfer = new Transfer(
				TO("+123456"),
				ANSWER_ON_MEDIA(false),
				INSTANCE(choices),
				FROM("12345"),
				INSTANCE(headers),
				NAME("transfer"),
				INSTANCE(
					new On(ARRAY(
						new On(
							EVENT("ring"),
							INSTANCE(new Say("http://www.phono.com/audio/holdmusic.mp3"))
						),
						new On(
							EVENT("connect"),
							INSTANCE(say)
						))
				  )
				),
				REQUIRED(false),
				TERMINATOR("*"),
				TIMEOUT(10),
				ALLOW_SIGNALS(false),
				INTER_DIGIT_TIMEOUT(10),
				INSTANCE(md)
			);

			Print(transfer.GetAction());

			Wait wait = new Wait(
				MILLISECONDS(500),
				ALLOW_SIGNALS(true)
			);

			Print(wait.GetAction());

			string resultJson = @"{""result"":{""callId"":""8a2bd2f204f4488e22c099a21e900068"",""sequence"":1,""calledid"":""6391721584130"",""sessionId"":""a1e275877b5346c576ae309cb2e6d829"",""state"":""ANSWERED"",""sessionDuration"":10,""complete"":true,""error"":null,""actions"":{""disposition"":""SUCCESS"",""interpretation"":""52521"",""xml"":""<?xml version=\""1.0\""?>\r\n<result grammar=\""0@39216af2.vxmlgrammar\"">\r\n    <interpretation grammar=\""0@39216af2.vxmlgrammar\"" confidence=\""100\"">\r\n        \r\n      <input mode=\""dtmf\"">dtmf-5 dtmf-2 dtmf-5 dtmf-2 dtmf-1<\/input>\r\n    <\/interpretation>\r\n<\/result>\r\n"",""confidence"":100,""name"":""foo"",""value"":""52521"",""utterance"":""52521"",""attempts"":1}}}";
			JObject resultParsed = JObject.Parse(resultJson);

			Result result = new Result(resultParsed);

			Print(result.GetResult());

			string sessionJson = @"{""session"":{""callId"":""e9d6ea5c3e1f9cdf7bd6755df792128f"",""accountId"":""357"",""headers"":{""P-Asserted-Identity"":""<sip:09065272450@112.198.80.86>"",""Call-ID"":""2b8e7d7ee5d-0002-0601@10.163.140.1"",""Session-Expires"":""1800"",""Max-Forwards"":""67"",""CSeq"":""22885 INVITE"",""Record-Route"":""<sip:54.251.186.109:5060;transport=udp;lr>"",""User-Agent"":""ZTE Softswitch/1.0.0"",""x-sid"":""d7cb81bdeef47e5d9520e3867e99dd2e"",""From"":""09065272450 <sip:6309065272450@112.198.80.86>;tag=aa38c01-u0HM7d7ee5c02"",""Supported"":""100rel"",""Contact"":""<sip:09065272450@112.198.80.86:5060>"",""Allow"":""INVITE"",""Via"":""SIP/2.0/UDP 54.251.186.109:5060;branch=z9hG4bK6vnl2t62s715;rport=5060;received=172.31.28.102"",""Min-SE"":""90"",""To"":""21584130 <sip:6391721584130@54.251.186.109>"",""Content-Length"":""790"",""P-Charging-Vector"":""icid-value=Netun-20161125172901-00580505"",""Content-Type"":""application/sdp""},""initialText"":null,""from"":{""name"":""09065272450"",""channel"":""VOICE"",""id"":""6309065272450"",""network"":""SIP""},""id"":""27741ecb65b5c916eeded4039ae22396"",""userType"":""HUMAN"",""to"":{""name"":""21584130"",""channel"":""VOICE"",""id"":""6391721584130"",""network"":""SIP""},""timestamp"":""2016-11-25T09:29:01.129Z""}}";
			JObject sessionParsed = JObject.Parse(sessionJson);

			Session session = new Session(sessionParsed);

			Print(session.GetSession());

			VoiceBase voice = new VoiceBase();

			voice.Say("Hello World");
			voice.Wait();

			Print(voice.Render().ToString());
		}

		static void Print(object data) {
			Console.WriteLine(data);
		}
	}
}
