using System.Net;

namespace SW.Services.Validate
{
    public abstract class ValidateService : Services
    {
        protected ValidateService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected ValidateService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestValidateXml(byte[] xml)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + "/validate/cfdi33");
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestValidateLrfc(string Lrfc)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + string.Format("lrfc/{0}", Lrfc));
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestValidateLco(string Lco)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + string.Format("lco/{0}", Lco));
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
    }
}
