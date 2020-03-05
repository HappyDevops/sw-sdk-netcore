using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SW.NetStandard20.Helpers.Extensions
{
    public  static class RequestExtensions
    {
        internal static string NormalizeBaseUrl(this string url)
        {
            if (url == null) return null;

            return !url.EndsWith("/") ? url + "/" : url;
        }

        internal static HttpWebRequest AddProxyToRequest(this HttpWebRequest request, string host, int port)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (host == null) throw new ArgumentNullException(nameof(host));
           
            var webProxy = new WebProxy(host, port);
            request.Proxy = webProxy;
            return request;
        }
    }
}
