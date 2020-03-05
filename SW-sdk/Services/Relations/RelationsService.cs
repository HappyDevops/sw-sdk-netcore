using SW.Helpers;
using System.IO;
using System.Net;

namespace SW.Services.Relations
{
    public abstract class RelationsService : Services
    {
        protected RelationsService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected RelationsService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract RelationsResponse RelationsRequest(string cer, string key, string rfc, string password, string uuid);
        internal abstract RelationsResponse RelationsRequest(byte[] xmlCancelation);
        internal abstract RelationsResponse RelationsRequest(string pfx, string rfc, string password, string uuid);
        internal abstract RelationsResponse RelationsRequest(string rfc, string uuid);
        internal virtual HttpWebRequest RequestRelations(string cer, string key, string rfc, string password, string uuid)
        {
            SetupRequest();
            string path = string.Format("relations/csd");
            var request = (HttpWebRequest)WebRequest.Create(Url + path);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RequestsCSD
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestRelations(byte[] xmlCancelation)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + "relations/xml");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            request.ContentLength = 0;
            RequestHelper.AddFileToRequest(xmlCancelation, ref request);
            RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestRelations(string pfx, string rfc, string password, string uuid)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + "relations/pfx");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, "bearer " + Token);
            RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RequestsPFX
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestRelations(string rfc, string uuid)
        {
            SetupRequest();
            string path = $"relations/{rfc}/{uuid}";
            var request = (HttpWebRequest)WebRequest.Create(Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
    }
}
