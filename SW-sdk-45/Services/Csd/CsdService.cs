using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SW.Services.Csd
{
    public abstract class CsdService : Services
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
        
        protected CsdService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected CsdService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract CsdResponse UploadCsd(string cer, string key, string password, string certificateType, bool isActive);
        internal abstract CsdResponse DisableCsd(string certificateNumber);
        internal abstract InfoCsdResponse InfoCsd(string certificateNumber);
        internal abstract InfoCsdResponse ActiveCsd(string rfc, string type);
        internal abstract ListInfoCsdResponse ListCsd();
        internal abstract ListInfoCsdResponse ListCsdByType(string type);
        internal abstract ListInfoCsdResponse ListCsdByRfc(string rfc);
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
            throw new NotImplementedException();
        }

        internal virtual StringContent RequestCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            SetupRequest();
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new UploadCsdRequest
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                type = certificateType,
                is_active = isActive
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
