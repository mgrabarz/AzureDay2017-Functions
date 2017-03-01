#r "Newtonsoft.Json"
#r "Twilio.Api"

using System;
using System.Net;
using Newtonsoft.Json;
using Twilio;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IAsyncCollector<SMSMessage> messages, TraceWriter log)
{
    var smsDetails = await req.Content.ReadAsAsync<SmsDetails>();

    if (smsDetails == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please provide sms details in the request body.");
    }
    else
    {
        var sms = new SMSMessage
        {
            Body = smsDetails.Body,
            To = smsDetails.To
        };
        messages.AddAsync(sms);
        return req.CreateResponse(HttpStatusCode.OK, "OK");
    }
}

public class SmsDetails
{
    public string To { get; set; }

    public string Body { get; set; }
}