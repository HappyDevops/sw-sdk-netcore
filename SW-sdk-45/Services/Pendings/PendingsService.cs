using System;
using System.Collections.Generic;
using System.Net;
using SW.Helpers;

namespace SW.Services.Pendings
{
    public abstract class PendingsService : Services
    {
        protected PendingsService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected PendingsService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
    }
}
