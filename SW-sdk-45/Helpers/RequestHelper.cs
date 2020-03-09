using System.Net;
using System.Net.Http;

namespace SW.Helpers
{
    internal class RequestHelper
    {
        internal static string NormalizeBaseUrl(string url)
        {
            return !url.EndsWith("/") ? url + "/" : url;
        }
        internal static void SetupProxy(string proxy, int port, ref HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(proxy))
            {
                WebProxy myproxy = new WebProxy(proxy, port);
                request.Proxy = myproxy;
            }
        }
        internal static HttpClientHandler ProxySettings(string proxy, int proxyPort)
        {
            if (!string.IsNullOrEmpty(proxy))
            {
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = new WebProxy($"{proxy}:{proxyPort}", false),
                    UseProxy = true
                };
                return httpClientHandler;
            }
            else
            {
                return new HttpClientHandler();
            }
        }
    }
}
