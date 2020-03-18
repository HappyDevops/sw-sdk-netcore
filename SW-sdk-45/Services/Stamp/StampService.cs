using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SW.Services.Stamp
{
    public abstract class StampService : Services
    {
        protected StampService(string url, string user, string password, string proxy, int proxyPort) : base(url, user,
            password, proxy, proxyPort)
        {

        }

        protected StampService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy,
            proxyPort)
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
                {"Authorization", "bearer " + Token}
            };
            return headers;
        }

        private void SetupRequest()
        {
        }

        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }

    }
}
