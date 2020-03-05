using System.Collections.Generic;
using System.Net;
using System.Net.Http;

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
        internal virtual MultipartFormDataContent GetMultipartContent(byte[] xml)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xml);
            content.Add(fileContent, "xml", "xml");
            return content;
        }
        internal virtual Dictionary<string, string> GetHeaders()
        {
            SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                    { "Authorization", "bearer " + Token }
                };
            return headers;
        }
        internal virtual HttpWebRequest RequestValidarLrfc(string lrfc)
        {
            SetupRequest();
            string path = string.Format("lrfc/{0}", lrfc);
            var request = (HttpWebRequest)WebRequest.Create(Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestValidarLco(string lco)
        {
            SetupRequest();
            string path = string.Format("lco/{0}", lco);
            var request = (HttpWebRequest)WebRequest.Create(Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
    }
}
