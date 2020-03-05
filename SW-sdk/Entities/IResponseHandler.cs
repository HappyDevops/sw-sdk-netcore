using System;
using System.Net;

namespace SW.Entities
{
    internal interface IResponseHandler
    {
        Entities.Response GetResponse(WebRequest request);
        Entities.Response HandleException(Exception ex);
    }
}
