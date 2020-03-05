using System.IO;
using System.Net;

namespace SW.Services.Cancelation
{
    public abstract class CancelationService : Services
    {
        protected CancelationService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected CancelationService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract CancelationResponse Cancelar(string cer, string key, string rfc, string password, string uuid);
        internal abstract CancelationResponse Cancelar(byte[] xmlCancelation);
        internal abstract CancelationResponse Cancelar(string pfx, string rfc, string password, string uuid);
        internal abstract CancelationResponse Cancelar(string rfc, string uuid);
        internal virtual HttpWebRequest RequestCancelar(string cer, string key, string rfc, string password, string uuid)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + "cfdi33/cancel/csd");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestCSD
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
        internal virtual HttpWebRequest RequestCancelar(string rfc, string uuid)
        {
            SetupRequest();
            string path = string.Format("cfdi33/cancel/{0}/{1}", rfc, uuid);
            var request = (HttpWebRequest)WebRequest.Create(Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestCancelar(string pfx, string rfc, string password, string uuid)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + "cfdi33/cancel/pfx");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestPFX
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
        internal virtual HttpWebRequest RequestCancelar(byte[] xmlCancelation)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + "cfdi33/cancel/xml");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            request.ContentLength = 0;
            Helpers.RequestHelper.AddFileToRequest(xmlCancelation, ref request);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }
    }
}
