
## Globe Connect for C Sharp

### Setting Up

```PM> Install-Package Globe.Connect```

### Authentication

#### Overview

If you haven't signed up yet, please follow the instructions found in [Getting Started](http://www.globelabs.com.ph/docs/#getting-started-create-an-app) to obtain an `App ID` and `App Secret` these tokens will be used to validate most of your interaction requests with the Globe APIs.

    The authenication process follows the protocols of **OAuth 2.0**. The example code below shows how you can swap your app tokens for an access token.

#### Sample Code

```csharp
using Globe.Connect;

Authentication auth = new Authentication([app_id], [app_secret]);

Console.WriteLine(auth.GetDialogUrl());

string code = "[code]";

Console.WriteLine(auth.GetAccessToken(code).GetDynamicResponse());
```

#### Sample Results

```json
{
    "access_token":"1ixLbltjWkzwqLMXT-8UF-UQeKRma0hOOWFA6o91oXw",
    "subscriber_number":"9171234567"
}
```

### SMS

#### Overview

Short Message Service (SMS) enables your application or service to send and receive secure, targeted text messages and alerts to your Globe / TM subscribers.

        Note: All API calls must include the access_token as one of the Universal Resource Identifier (URI) parameters.

#### SMS Sending

Send an SMS message to one or more mobile terminals:

##### Sample Code

```csharp
using Globe.Connect;

Sms sms = new Sms([short_code], [acces_token]);

dynamic response = sms
    .SetReceiverAddress("[subscriber_number]")
    .SetMessage("[message]")
    .SendMessage()
    .GetDynamicResponse();

Console.WriteLine(response);
```

##### Sample Results

```json
{
    "outboundSMSMessageRequest": {
        "address": "tel:+639175595283",
        "deliveryInfoList": {
            "deliveryInfo": [],
            "resourceURL": "https://devapi.globelabs.com.ph/smsmessaging/v1/outbound/8011/requests?access_token=3YM8xurK_IPdhvX4OUWXQljcHTIPgQDdTESLXDIes4g"
        },
        "senderAddress": "8011",
        "outboundSMSTextMessage": {
            "message": "Hello World"
        },
        "receiptRequest": {
            "notifyURL": "http://test-sms1.herokuapp.com/callback",
            "callbackData": null,
            "senderName": null,
            "resourceURL": "https://devapi.globelabs.com.ph/smsmessaging/v1/outbound/8011/requests?access_token=3YM8xurK_IPdhvX4OUWXQljcHTIPgQDdTESLXDIes4g"
        }
    }
}
```

#### SMS Binary

Send binary data through SMS:

##### Sample Code

```csharp
using Globe.Connect;

BinarySms sms = new BinarySms([short_code], [access_token]);

dynamic response = sms
    .SetReceiverAddress("[subscriber_number]")
    .SetUserDataHeader("[data_header]")
    .SetDataCodingScheme([coding_scheme])
    .SetBinaryMessage("[message]")
    .SendBinaryMessage()
    .GetDynamicResponse();

Console.WriteLine(response);
```

##### Sample Results

```json
{
    "outboundBinaryMessageRequest": {
        "address": "9171234567",
        "deliveryInfoList": {
            "deliveryInfo": [],
            "resourceURL": "https://devapi.globelabs.com.ph/binarymessaging/v1/outbound/{senderAddress}/requests?access_token={access_token}",
        "senderAddress": "21581234",
        "userDataHeader": "06050423F423F4",
        "dataCodingScheme": 1,
        "outboundBinaryMessage": {
            "message": "samplebinarymessage"
        },
        "receiptRequest": {
          "notifyURL": "http://example.com/notify",
          "callbackData": null,
          "senderName": null
        },
        "resourceURL": "https://devapi.globelabs.com.ph/binarymessaging/v1/outbound/{senderAddress}/requests?access_token={access_token}"
    }
}
```

#### SMS Mobile Originating (SMS-MO)

Receiving an SMS from globe (Mobile Originating - Subscriber to Application):

##### Sample Code

```csharp
using Newtonsoft.Json.Linq;

...

public ActionResult Index()
{
	String data = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
	JObject result = JObject.Parse(data);

	Console.WriteLine(result);

	return Content(result.ToString(), "application/json");
}
```

##### Sample Results

```json
{
  "inboundSMSMessageList":{
      "inboundSMSMessage":[
         {
            "dateTime":"Fri Nov 22 2013 12:12:13 GMT+0000 (UTC)",
            "destinationAddress":"tel:21581234",
            "messageId":null,
            "message":"Hello",
            "resourceURL":null,
            "senderAddress":"9171234567"
         }
       ],
       "numberOfMessagesInThisBatch":1,
       "resourceURL":null,
       "totalNumberOfPendingMessages":null
   }
}
```

### Voice

#### Overview

The Globe APIs has a detailed list of voice features you can use with your application.

#### Voice Ask

You can take advantage of Globe's automated Ask protocols to help service your customers without manual intervention for common questions in example.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

...

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
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API."
            }
        },
        {
            ask: {
                choices: {
                    value: "[5 DIGITS]"
                },
                attempts: 3,
                bargein: false,
                name: "foo",
                required: true,
                say: {
                    value: "Please enter your 5 digit zip code."
                },
                timeout: 10
            }
        },
        {
            on: {
                event: "continue",
                next: "http://somefakehost.com:8000/",
                required: true
            }
        }
    ]
}
```

#### Voice Answer

You can take advantage of Globe's automated Ask protocols to help service your customers without manual intervention for common questions in example.

##### Sample Code

```csharp
using Globe.Connect.Voice;

...

public ActionResult Index()
{
    Voice voice = new Voice();

    voice.Say("Welcome to my Tropo Web API.");
    voice.Hangup();

    return Content(voice.Render().ToString(), "application/json");
}
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API."
            }
        },
        {
            hangup: { }
        }
    ]
}
```

#### Voice Ask Answer

A better sample of the Ask and Answer dialog would look like the following.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

using Newtonsoft.Json.Linq;

...

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
```

##### Sample Results

```json
if path is ask?

{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API."
            }
        },
        {
            ask: {
                choices: {
                    value: "[5 DIGITS]"
                },
                attempts: 3,
                bargein: false,
                name: "foo",
                required: true,
                say: {
                    value: "Please enter your 5 digit zip code."
                },
                timeout: 10
            }
        },
        {
            on: {
                event: "continue",
                next: "/askanswer/answer",
                required: true
            }
        }
    ]
}

if path is answer?

{
    tropo: [
        {
            say: {
                value: "Your zip code is 52521, thank you!"
            }
        }
    ]
}
```

#### Voice Call

You can connect your app to also call a customer to initiate the Ask and Answer features.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

...

public ActionResult Index()
{
    Voice voice = new Voice();

    voice.Call(
        TO("9065272450"),
        FROM("sip:21584130@sip.tropo.net")
    );

    voice.Say(ARRAY(new Say("Hello World")));

    return Content(voice.Render().ToString(), "application/json");
}
```

##### Sample Results

```json
{
    tropo: [
        {
            call: {
                to: "9065272450",
                from: "sip:21584130@sip.tropo.net"
            }
        },
        [
            {
                value: "Hello World"
            }
        ]
    ]
}
```

#### Voice Conference

You can take advantage of Globe's automated Ask protocols to help service your customers without manual intervention for common questions in example.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

...

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
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API Conference Call."
        }
        },
        {
            conference: {
                id: "12345",
                mute: false,
                name: "foo",
                playTones: true,
                terminator: "#",
                joinPrompt: {
                    value: "http://openovate.com/hold-music.mp3"
                },
                leavePrompt: {
                    value: "http://openovate.com/hold-music.mp3"
                }
            }
        }
    ]
}
```

#### Voice Event

Call events are triggered depending on the response of the receiving person. Events are used with the Ask and Answer features.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

...

public ActionResult Index()
{
    Voice voice = new Voice();

    voice.Say("Welcome to my Tropo Web API.");

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

    voice.Ask(
        INSTANCE(choices),
        ATTEMPTS(3),
        BARGEIN(false),
        NAME("foo"),
        REQUIRED(true),
        INSTANCE(say),
        TIMEOUT(5)
    );

    voice.On(
        EVENT("continue"),
        NEXT("http://somefakehost:8000/"),
        REQUIRED(true)
    );

    return Content(voice.Render().ToString(), "application/json");
}
```

##### Sample Results

```json
{
tropo: [
    {
        say: {
            value: "Welcome to my Tropo Web API."
        }
    },
    {
        ask: {
                choices: {
                    value: "[5 DIGITS]"
                },
                attempts: 3,
                bargein: false,
                name: "foo",
                required: true,
                say: [
                    {
                        value: "Sorry, I did not hear anything.",
                        event: "timeout"
                    },
                    {
                        value: "Sorry, that was not a valid option.",
                        event: "nomatch:1"
                    },
                    {
                        value: "Nope, still not a valid response",
                        event: "nomatch:2"
                    },
                    {
                        value: "Please enter your 5 digit zip code."
                    }
                ],
                timeout: 5
            }
        },
        {
            on: {
                event: "continue",
                next: "http://somefakehost:8000/",
                required: true
            }
        }
    ]
}
```

#### Voice Hangup

Between your automated dialogs (Ask and Answer) you can automatically close the voice call using this feature. 

##### Sample Code

```csharp
using Globe.Connect.Voice;

...

public ActionResult Index()
{
    Voice voice = new Voice();

    voice.Say("Welcome to my Tropo Web API, thank you!");
    voice.Hangup();

    return Content(voice.Render().ToString(), "application/json");
}
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API, thank you!"
            }
        },
        {
            hangup: { }
        }
    ]
}
```

#### Voice Record

It is helpful to sometime record conversations, for example to help improve on the automated dialog (Ask and Answer). The following sample shows how you can use connect your application with voice record features.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using Globe.Connect.Voice.Enums;
using static Globe.Connect.Voice.Actions.Key;

using VoiceBase = Globe.Connect.Voice.Voice;

...

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
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API."
            }
        },
        {
            record: {
                attempts: 3,
                bargein: false,
                method: "POST",
                required: true,
                say: [
                    {
                        value: "Sorry, I did not hear anything. Please call back.",
                        event: "timeout"
                    },
                    {
                        value: "Please leave a message"
                    }
                ],
                name: "foo",
                url: "http://openovate.com/globe.php",
                format: "audio/wav",
                choices: {
                    terminator: "#"
                },
                transcription: {
                    id: "1234",
                    url: "mailto:charles.andacc@gmail.com"
                }
            }
        }
    ]
}
```

#### Voice Reject

To filter incoming calls automatically, you can use the following example below. 

##### Sample Code

```csharp
using Globe.Connect.Voice;

...

public ActionResult Index()
{
    Voice voice = new Voice();

    voice.Reject();

    return Content(voice.Render().ToString(), "application/json");
}
```

##### Sample Results

```json
{
    tropo: [
        {
            reject: { }
        }
    ]
}
```

#### Voice Routing

To help integrate Globe Voice with web applications, this API using routing which can be easily routed within your framework.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using static Globe.Connect.Voice.Actions.Key;

...

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
```

##### Sample Results

```json
if path is routing?

{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API."
            }
        },
        {
            on: {
                next: "/VoiceSample/RoutingTest1",
                event: "continue"
            }
        }
    ]
}

if path is routing1?

{
    tropo: [
        {
            say: {
                value: "Hello from resource one!"
            }
        },
        {
            on: {
                next: "/VoiceSample/RoutingTest2",
                event: "continue"
            }
        }
    ]
}

if path is routing2?

{
    tropo: [
        {
            say: {
                value: "Hello from resource two! thank you."
            }
        }
    ]
}
```

#### Voice Say

The message you pass to `say` will be transformed to an automated voice.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using static Globe.Connect.Voice.Actions.Key;

...

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
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API."
            }
        },
        {
            say: {
                value: "I will play an audio file for you, please wait."
            }
        },
        {
            say: {
                value: "http://openovate.com/tropo-rocks.mp3"
            }
        }
    ]
}
```

#### Voice Transfer

The following sample explains the dialog needed to transfer the receiver to another phone number.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using static Globe.Connect.Voice.Actions.Key;

...

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
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API, you are now being transferred."
            }
        },
        {
            transfer: {
                to: "9053801178",
                ringRepeat: 2,
                on: [
                    {
                        event: "ring",
                        say: {
                            value: "http://openovate.com/hold-music.mp3"
                        }
                    },
                    {
                        event: "connect",
                        ask: {
                            choices: {
                                value: "[5 DIGITS]"
                            },
                            attempts: 3,
                            bargein: false,
                            name: "foo",
                            required: true,
                            say: [
                                {
                                    value: "Sorry, I did not hear anything.",
                                    event: "timeout"
                                },
                                {
                                    value: "Sorry, that was not a valid option.",
                                    event: "nomatch:1"
                                },
                                {
                                    value: "Nope, still not a valid response",
                                    event: "nomatch:2"
                                },
                                {
                                    value: "Please enter your 5 digit zip code."
                                }
                            ],
                            timeout: 5
                        }
                    }
                ]
            }
        }
    ]
}
```

#### Voice Transfer Whisper

TODO

##### Sample Code

```csharp
using Globe.Connect.Voice;
using Globe.Connect.Voice.Actions;
using Globe.Connect.Voice.Enums;
using static Globe.Connect.Voice.Actions.Key;

using VoiceBase = Globe.Connect.Voice.Voice;

...

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
```

##### Sample Results

```json
if transfer whisper?

{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API, please hold while you are being transferred."
            }
        },
        {
            transfer: {
                to: "9054799241",
                name: "foo",
                on: [
                    {
                        event: "ring",
                        say: {
                            value: "http://openovate.com/hold-music.mp3"
                        }
                    },
                    {
                        event: "connect",
                        ask: {
                            choices: {
                                value: "1",
                                mode: "dtmf"
                            },
                            name: "color",
                            say: {
                                value: "Press 1 to accept this call or any other number to reject"
                            },
                            timeout: 60
                        }
                    },
                    {
                        event: "connect",
                        say: {
                            value: "You are now being connected."
                        }
                    }
                ],
                required: true,
                terminator: "*"
            }
        },
        {
            on: {
                event: "incomplete",
                next: "/transferwhisper/hangup",
                say: {
                    value: "You are now being disconnected."
                }
            }
        }
    ]
}

if hangup?

{
    tropo: [
        {
            hangup: { }
        }
    ]
}
```

#### Voice Wait

To put a receiver on hold, you can use the following sample.

##### Sample Code

```csharp
using Globe.Connect.Voice;
using static Globe.Connect.Voice.Actions.Key;

...

public ActionResult Index()
{
    Voice voice = new Voice();

    voice.Say("Welcome to my Tropo Web API, please wait for a while.");
    voice.Wait(MILLISECONDS(5000), ALLOW_SIGNALS(true));
    voice.Say("Thank you for waiting!");

    return Content(voice.Render().ToString(), "application/json");
}
```

##### Sample Results

```json
{
    tropo: [
        {
            say: {
                value: "Welcome to my Tropo Web API, please wait for a while."
            }
        },
        {
            wait: {
                milliseconds: 5000,
                allowSignals: true
            }
        },
        {
            say: {
                value: "Thank you for waiting!"
            }
        }
    ]
}
```

### USSD

#### Overview

USSD are basic features built on most smart phones which allows the phone owner to interact with menu item choices.

#### USSD Sending

The following example shows how to send a USSD request.

##### Sample Code

```csharp
using Globe.Connect;

Ussd ussd = new Ussd([access_token]);

dynamic response = ussd
    .SetAddress("[subscriber_number]")
    .SetSenderAddress([short_code])
    .SetUssdMessage("[message]")
    .SetFlash([flash])
    .SendUssdRequest()
    .GetDynamicResponse();

Console.WriteLine(response);
```

##### Sample Results

```json
{
    "outboundUSSDMessageRequest": {
        "address": "639954895489",
        "deliveryInfoList": {
            "deliveryInfo": [],
            "resourceURL": "https://devapi.globelabs.com.ph/ussd/v1/outbound/21589996/reply/requests?access_token=access_token"
        },
        "senderAddress": "21580001",
        "outboundUSSDMessage": {
            "message": "Simple USSD Message\nOption - 1\nOption - 2"
        },
        "receiptRequest": {
            "ussdNotifyURL": "http://example.com/notify",
            "sessionID": "012345678912"
        },
        "resourceURL": "https://devapi.globelabs.com.ph/ussd/v1/outbound/21589996/reply/requests?access_token=access_token"
    }
}
```

#### USSD Replying

The following example shows how to send a USSD reply.

##### Sample Code

```csharp
using Globe.Connect;

Ussd ussd = new Ussd([access_token]);

try {
    response = ussd
        .SetAddress("[subscriber_number]")
        .SetSessionId([session_id])
        .SetSenderAddress([short_code])
        .SetUssdMessage("[message]")
        .SetFlash([flash])
        .ReplyUssdRequest()
        .GetDynamicResponse();

    Console.WriteLine(response);
} catch(WebException e) {
    Console.WriteLine(new System.IO.StreamReader(e.Response.GetResponseStream()).ReadToEnd());
}
```

##### Sample Results

```json
{
    "outboundUSSDMessageRequest": {
        "address": "639954895489",
        "deliveryInfoList": {
            "deliveryInfo": [],
            "resourceURL": "https://devapi.globelabs.com.ph/ussd/v1/outbound/21589996/reply/requests?access_token=access_token"
        },
        "senderAddress": "21580001",
        "outboundUSSDMessage": {
            "message": "Simple USSD Message\nOption - 1\nOption - 2"
        },
        "receiptRequest": {
            "ussdNotifyURL": "http://example.com/notify",
            "sessionID": "012345678912",
            "referenceID": "f7b61b82054e4b5e"
        },
        "resourceURL": "https://devapi.globelabs.com.ph/ussd/v1/outbound/21589996/reply/requests?access_token=access_token"
    }
}
```

### Payment

#### Overview

Your application can monetize services from customer's phone load by sending a payment request to the customer, in which they can opt to accept.

#### Payment Requests

The following example shows how you can request for a payment from a customer.

##### Sample Code

```csharp
using Globe.Connect;

Payment payment = new Payment([app_id], [app_secret], [access_token]);

dynamic response = payment
    .SetAmount([amount])
    .SetDescription("[description]")
    .SetEndUserId("[subscriber_number]")
    .SetReferenceCode("[reference]")
    .SetTransactionOperationStatus("[status]")
    .SendPaymentRequest()
    .GetDynamicResponse();

Console.WriteLine(response);
```

##### Sample Results

```json
{
    "amountTransaction":
    {
        "endUserId": "9171234567",
        "paymentAmount":
        {
            "chargingInformation":
            {
                "amount": "0.00",
                "currency": "PHP",
                "description": "my application"
            },
            "totalAmountCharged": "0.00"
        },
        "referenceCode": "12341000023",
        "serverReferenceCode": "528f5369b390e16a62000006",
        "resourceURL": null
    }
}
```

#### Payment Last Reference

The following example shows how you can get the last reference of payment.

##### Sample Code

```csharp
using Globe.Connect;

Payment payment = new Payment([app_id], [app_secret], [access_token]);

response = payment
    .GetLastReferenceCode()
    .GetDynamicResponse();

Console.WriteLine(response);
```

##### Sample Results

```json
{
    "referenceCode": "12341000005",
    "status": "SUCCESS",
    "shortcode": "21581234"
}
```

### Amax

#### Overview

Amax is an automated promo builder you can use with your app to award customers with certain globe perks.

#### Sample Code

```csharp
using Globe.Connect;

Amax amax = new Amax([app_id], [app_secret]);

try {
    dynamic response = amax
        .SetAddress("[subscriber_number]")
        .SetRewardsToken("[rewards_token]")
        .SetPromo("[promo]")
        .SendRewardRequest()
        .GetDynamicResponse();

    Console.WriteLine(response);
} catch(WebException e) {
    Console.WriteLine(new System.IO.StreamReader(e.Response.GetResponseStream()).ReadToEnd());
}
```

#### Sample Results

```json
{
    "outboundRewardRequest": {
        "transaction_id": 566,
        "status": "Please check your AMAX URL for status",
        "address": "9065272450",
        "promo": "FREE10MB"
    }
}
```

### Location

#### Overview

To determine a general area (lat,lng) of your customers you can utilize this feature.

#### Sample Code

```csharp
using Globe.Connect;

Location location = new Location([access_token]);

dynamic response = location
    .SetAddress("[subscriber_number]")
    .SetRequestedAccuracy([accuracy])
    .GetLocation()
    .GetDynamicResponse();

Console.WriteLine(response);
```

#### Sample Results

```json
{
    "terminalLocationList": {
        "terminalLocation": {
            "address": "tel:9171234567",
            "currentLocation": {
                "accuracy": 100,
                "latitude": "14.5609722",
                "longitude": "121.0193394",
                "map_url": "http://maps.google.com/maps?z=17&t=m&q=loc:14.5609722+121.0193394",
                "timestamp": "Fri Jun 06 2014 09:25:15 GMT+0000 (UTC)"
            },
            "locationRetrievalStatus": "Retrieved"
        }
    }
}
```

### Subscriber

#### Overview

Subscriber Data Query API interface allows a Web application to query the customer profile of an end user who is the customer of a mobile network operator.

#### Subscriber Balance

The following example shows how you can get the subscriber balance.

##### Sample Code

```csharp
using Globe.Connect;

Subscriber subscriber = new Subscriber([access_token]);

dynamic response = subscriber
    .SetAddress("[subscriber_number]")
    .GetSubscriberBalance()
    .GetDynamicResponse();

Console.WriteLine(response);
```

##### Sample Results

```json
{
    terminalLocationList:
    {
        terminalLocation:
        [
            {
                address: "639171234567",
                subBalance: "60200"
            }
        ]
    }
}
```

#### Subscriber Reload

The following example shows how you can get the subscriber reload amount.

##### Sample Code

```csharp
using Globe.Connect;

Subscriber subscriber = new Subscriber([access_token]);

response = subscriber
    .SetAddress("[subscriber_number]")
    .GetSubscriberReloadAmount()
    .GetDynamicResponse();

Console.WriteLine(response);
```

##### Sample Results

```json
{
    terminalLocationList:
    {
        terminalLocation:
        [
            {
                address: "639171234567",
                reloadAmount: "30000"
            }
        ]
    }
}
```
