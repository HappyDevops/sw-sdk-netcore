﻿using System;
using System.Text;


namespace SW.Services.Validate
{
    public abstract class BaseValidate : ValidateService
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
        
        private string _operation;
        public BaseValidate(string url, string user, string password, string operation, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        public BaseValidate(string url, string token, string operation, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
        }
        public virtual ValidateXmlResponse ValidateXml(string XML)
        {
            ValidateXmlResponseHandler handler = new ValidateXmlResponseHandler();
            try
            {
                var xmlBytes = Encoding.UTF8.GetBytes(XML);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes);
                var proxy = Helpers.RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                string.Format("validate/cfdi33/",
                                _operation), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual ValidateLcoResponse ValidateLco(string Lco)
        {
            ValidateLcoResponseHandler handler = new ValidateLcoResponseHandler();
            try
            {
                var headers = GetHeaders();
                var content = RequestValidarLco(Lco);
                var proxy = Helpers.RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetResponse(Url,
                                headers,
                                string.Format("lco/{0}", Lco),
                                proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }

        public virtual ValidateLrfcResponse ValidateLrfc(string Lrfc)
        {
            ValidateLrfcResponseHandler handler = new ValidateLrfcResponseHandler();
            try
            {
                var headers = GetHeaders();
                var content = RequestValidarLrfc(Lrfc);
                var proxy = Helpers.RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetResponse(Url,
                                headers,
                                string.Format("lrfc/{0}", Lrfc),
                                proxy
                                );
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
