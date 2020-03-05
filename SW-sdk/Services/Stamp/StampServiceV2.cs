using System.Net;

namespace SW.Services.Stamp
{
    public abstract class StampServiceV2 : Services
    {
        protected StampServiceV2(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected StampServiceV2(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestStamping(byte[] xml, string version, string format, string operation)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + string.Format("cfdi33/v2/{0}/{1}/{2}", operation, version, format));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }


    }
}
