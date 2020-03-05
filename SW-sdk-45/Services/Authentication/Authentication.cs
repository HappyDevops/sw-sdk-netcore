using System;
using System.Collections.Generic;

namespace SW.Services.Authentication
{
    public class Authentication : AuthenticationService
    {
        readonly AuthenticationResponseHandler _handler;
        public Authentication(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new AuthenticationResponseHandler();
        }
        public override AuthResponse GetToken()
        {
            try
            {
                new AuthenticationValidation(Url, User, Password, Token);

                var headers = new Dictionary<string, string>
                {
                    { "user", User },
                    { "password", Password }
                };
                var proxy = Helpers.RequestHelper.ProxySettings(Proxy, ProxyPort);
                return _handler.GetPostResponse(Url, headers, "security/authenticate", proxy);

            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
