using System;
using System.Net;

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
        
        internal static HttpWebRequest AddAuthorizationHeader(this HttpWebRequest request, string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), $"bearer {token}");
            return request;
        }
        
        internal static HttpWebRequest PrepareForGETMethod(this HttpWebRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Get;

            return request;
        }
    }
}
