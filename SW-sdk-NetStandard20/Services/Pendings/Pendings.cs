using System;
using System.Collections.Generic;
using System.Net;
using SW.NetStandard20.Exceptions;
using SW.NetStandard20.Helpers.Extensions;
using SW.NetStandard20.Services.Parameters;

namespace SW.NetStandard20.Services.Pendings
{
    public sealed class Pendings 
    {
        public ISWServiceCredentials Credentials { get; }
        private const string GET_PENDINGS_SERVICE_PATH = "pendings/";
        private DateTime _expirationDate;
        private const int _timeSession = 2;

        public Pendings()
        {

        }

        public Pendings(ISWServiceCredentials credentials)
        {
            Credentials = credentials;
        }

        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
        public DateTime ExpirationDate => _expirationDate;

        private PendingsResponse CreateHttpRequest(string rfc)
        {
            var request = RequestPendings(rfc);
            try
            {
                request = request.PrepareForGETMethod();
                request = request.AddProxyToRequest("", 0);
                request = request.AddAuthorizationHeader(null);

                
                var headers = GetHeaders();
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetResponse(Url, headers, $"{GET_PENDINGS_SERVICE_PATH}{rfc}", proxy);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleError(e, );
                return new PendingsResponse(e);
            }
        }

        private 

        public PendingsResponse PendingsByRfc(string rfc)
        {
            return CreateHttpRequest(rfc);
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
            request = request.PrepareForGETMethod();
            request = request.AddAuthorizationHeader(Token);
            request = request.AddProxyToRequest(Proxy,ProxyPort);
            return request;
        }

        public voi SetupRequest()
        {
            if (!string.IsNullOrEmpty(Token) && DateTime.Now <= ExpirationDate) return this;

            var auth = new Authentication.Authentication(Url, User, Password, ProxyPort, Proxy);
            var response = auth.GetToken();

            if (response.status != ResponseType.success.ToString()) return this;

            Token = response.data.token;
            _expirationDate = DateTime.Now.AddHours(_timeSession);
            return this;
        }
    }
}

