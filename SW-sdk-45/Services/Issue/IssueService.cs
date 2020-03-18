using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SW.Services.Issue
{
    public abstract class IssueService : Services
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
       
        protected IssueService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected IssueService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestStampJson(string json, string version, string operation)
        {
            SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + string.Format("v3/cfdi33/{0}/{1}", operation, version));
            request.ContentType = "application/jsontoxml";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            request.ContentLength = json.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }

        private void SetupRequest()
        {
            throw new System.NotImplementedException();
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


    }
}
