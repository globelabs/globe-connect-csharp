# Globe Connect for CS

## Introduction
Globe Connect for C# provides an implementation of Globe APIs e.g Authentication, Amax,
Sms etc. that is easy to use and can be integrated in your existing C# application. Below shows
some samples on how to use the API depending on the functionality that you need to integrate in your
application.

## Basic Usage

###### Figure 1. Authentication

```cs
using Globe.Connect;

Authentication auth = new Authentication(APP_ID, APP_SECRET);

Console.WriteLine("Get dialog url:");
Console.WriteLine(auth.GetDialogUrl());

string code = "[code]";

Console.WriteLine("Get access token:");
Console.WriteLine(auth.GetAccessToken(code).GetDynamicResponse());
```

###### Figure 2. Amax

```cs
using Globe.Connect;

Amax amax = new Amax(APP_ID, APP_SECRET);

Console.WriteLine("Send reward request:");

try {
    dynamic response = amax
        .SetAddress("[+63 subscriber_number]")
        .SetRewardsToken("[rewards_token]")
        .SetPromo("[promo]")
        .SendRewardRequest()
        .GetDynamicResponse();

    Console.WriteLine("Response: -->");
    Console.WriteLine(response);
} catch(WebException e) {
    Console.WriteLine(new System.IO.StreamReader(e.Response.GetResponseStream()).ReadToEnd());
}
```

###### Figure 3. Binary SMS

```cs
using Globe.Connect;

BinarySms sms = new BinarySms(SHORT_CODE, ACCESS_TOKEN);

Console.WriteLine("Sending Binary SMS:");

dynamic response = sms
    .SetReceiverAddress("[+63 subscriber_number]")
    .SetUserDataHeader("[data_header]")
    .SetDataCodingScheme([scheme])
    .SetBinaryMessage("[message]")
    .SendBinaryMessage()
    .GetDynamicResponse();

Console.WriteLine("Response: -->");
Console.WriteLine(response);
```

###### Figure 4. Location

```cs
using Globe.Connect;

Location location = new Location(ACCESS_TOKEN);

Console.WriteLine("Get Location request:");

dynamic response = location
    .SetAddress("[+63 subscriber_number]")
    .SetRequestedAccuracy([accuracy])
    .GetLocation();

Console.WriteLine("Response: -->");
Console.WriteLine(response);
```

###### Figure 5. Payment (Send Payment Request)

```cs
using Globe.Connect;

Payment payment = new Payment(APP_ID, APP_SECRET, ACCESS_TOKEN);

Console.WriteLine("Sending payment request:");

dynamic response = payment
    .SetAmount([amount])
    .SetDescription("[description]")
    .SetEndUserId("[+63 subscriber_number]")
    .SetReferenceCode("[reference_code]")
    .SetTransactionOperationStatus("[status]")
    .SendPaymentRequest()
    .GetDynamicResponse();

Console.WriteLine("Response: -->");
Console.WriteLine(response);
```

###### Figure 6. Payment (Get Last Reference ID)

```cs
using Globe.Connect;

Payment payment = new Payment(APP_ID, APP_SECRET, ACCESS_TOKEN);

Console.WriteLine("Get last reference code request:");

response = payment
    .GetLastReferenceCode()
    .GetDynamicResponse();

Console.WriteLine("Response: --> ");
Console.WriteLine(response);
```

###### Figure 7. Sms

```cs
using Globe.Connect;

Sms sms = new Sms(SHORT_CODE, ACCESS_TOKEN);

Console.WriteLine("Sending SMS:");

dynamic response = sms
    .SetReceiverAddress("[+63 subscriber_number]")
    .SetMessage("[message]")
    .SendMessage()
    .GetDynamicResponse();

Console.WriteLine("Response: -->");
Console.WriteLine(response);
```

###### Figure 8. Subscriber (Get Balance)

```cs
using Globe.Connect;

Subscriber subscriber = new Subscriber(ACCESS_TOKEN);

Console.WriteLine("Get subscriber balance request:");

dynamic response = subscriber
    .SetAddress("[+63 subscriber_number]")
    .GetSubscriberBalance()
    .GetDynamicResponse();

Console.WriteLine("Response: -->");
Console.WriteLine(response);
```

###### Figure 9. Subscriber (Get Reload Amount)

```cs
using Globe.Connect;

Subscriber subscriber = new Subscriber(ACCESS_TOKEN);

Console.WriteLine("Get subscriber reload amount request:");

response = subscriber
    .SetAddress("+[+63 subscriber_number]")
    .GetSubscriberReloadAmount()
    .GetDynamicResponse();

Console.WriteLine("Response: -->");
Console.WriteLine(response);
```

###### Figure 10. USSD (Send)

```cs
using Globe.Connect;

Ussd ussd = new Ussd(ACCESS_TOKEN);

Console.WriteLine("Send ussd request:");

dynamic response = ussd
    .SetAddress("[+63 subscriber_number]")
    .SetSenderAddress(SHORT_CODE)
    .SetUssdMessage("[message]")
    .SetFlash([flash])
    .SendUssdRequest()
    .GetDynamicResponse();

Console.WriteLine("Response: -->");

Console.WriteLine(response);
```

###### Figure 11. USSD (Reply)

```cs
using Globe.Connect;

Ussd ussd = new Ussd(ACCESS_TOKEN);

Console.WriteLine("Reply ussd request:");

try {
    response = ussd
        .SetAddress("[+63 subscriber_number]")
        .SetSessionId(sessionId)
        .SetSenderAddress(SHORT_CODE)
        .SetUssdMessage("[message]")
        .SetFlash([flash])
        .ReplyUssdRequest()
        .GetDynamicResponse();

    Console.WriteLine("Response: -->");
    Console.WriteLine(response);

} catch(WebException e) {
    Console.WriteLine(new System.IO.StreamReader(e.Response.GetResponseStream()).ReadToEnd());
}
```
