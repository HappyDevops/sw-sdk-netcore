using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace SW.Services.Pdf
{
    public abstract class PdfService : Services
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
      

        protected PdfService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected PdfService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }

        internal virtual MultipartFormDataContent GetMultipartContent(byte[] xml, Dictionary<string, string> ObservacionesAdcionales)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xml);
            content.Add(fileContent, "xml", "xml");
            content.Add(new StringContent(JsonConvert.SerializeObject(ObservacionesAdcionales, Formatting.Indented)), "extras");
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

        private void SetupRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}
