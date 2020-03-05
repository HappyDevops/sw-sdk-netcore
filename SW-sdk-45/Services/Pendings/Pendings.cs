using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Net;

namespace SW.Services.Pendings
{
    public sealed class Pending : PendingsService
    {
        private DateTime _expirationDate;
        private int _timeSession = 2;

        public Pending(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
        }
        public Pending(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
        }

        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
        public DateTime ExpirationDate => _expirationDate;

        internal PendingsResponse PendingsRequest(string rfc)
        {
            PendingsResponseHandler handler = new PendingsResponseHandler();
            var request = RequestPendings(rfc);
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Get;
                var headers = GetHeaders();
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetResponse(Url, headers, $"pendings/{rfc}", proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        public PendingsResponse PendingsByRfc(string rfc)
        {
            return PendingsRequest(rfc);
        }

        internal Dictionary<string, string> GetHeaders()
        {
            SetupRequest();
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"bearer {Token}"}
            };
            return headers;
        }

        internal HttpWebRequest RequestPendings(string rfc)
        {
            SetupRequest();
            var path = $"pendings/{rfc}";
            var request = (HttpWebRequest)WebRequest.Create(Url + path);
            request = request.prepa
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), $"bearer {Token}");
            Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }

        public Services SetupRequest()
        {
            if (!string.IsNullOrEmpty(Token) && DateTime.Now <= ExpirationDate) return this;

            var auth = new Authentication.Authentication(Url,User,Password, ProxyPort, Proxy);
            var response = auth.GetToken();

            if (response.status != ResponseType.success.ToString()) return this;

            Token = response.data.token;
            _expirationDate = DateTime.Now.AddHours(_timeSession);
            return this;
        }
    }
}
