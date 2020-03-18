using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
       
        private string _operation;
        public BasePdf(string url, string token, string operation, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
        }
        public BasePdf(string url, string user, string password, string operation, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        public virtual PdfResponse GenerarPdf(string xml, string templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                string format = isB64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes, ObservacionesAdicionales);
                var proxy = Helpers.RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                string.Format("/pdf/v1/generate",
                                _operation), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }

        }
    }
}
