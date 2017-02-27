using System;
using System.Net;

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log)
{
    return req.CreateResponse(HttpStatusCode.OK,
        new { Message = "Hello World from B!", Version = "v1" });
}
