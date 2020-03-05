﻿using System;
using System.Net;

namespace SW.Services.Authentication
{
    public class Authentication : AuthenticationService
    {
        AuthenticationResponseHandler _handler;
        public Authentication(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new AuthenticationResponseHandler();
        }
        public override AuthResponse GetToken()
        {
            try
            {
                new AuthenticationValidation(Url, User, Password, Token);
                var request = (HttpWebRequest)WebRequest.Create(Url + "security/authenticate");
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                request.Headers.Add("user", User);
                request.Headers.Add("password", Password);
                Helpers.RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
                return _handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
