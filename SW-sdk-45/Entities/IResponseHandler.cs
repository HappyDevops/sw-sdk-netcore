using System;
using System.Net;

namespace SW.Entities
{
    internal interface IResponseHandler
    {
        Response GetResponse(WebRequest request);
        Response HandleException(Exception ex);
    }
}
