﻿using System;
using SW.Helpers;
using System.Net;

namespace SW.Services.Cancelation
{
    public class Cancelation : CancelationService
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
        

        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public Cancelation(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
        }
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        public Cancelation(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
        }

        internal override CancelationResponse Cancelar(string cer, string key, string rfc, string password, string uuid)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = RequestCancelar(cer, key, rfc, password, uuid);
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                "cfdi33/cancel/csd", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(string rfc, string uuid)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = RequestCancelar(rfc, uuid);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                var headers = GetHeaders();
                return handler.GetPostResponse(Url, headers, $"cfdi33/cancel/{rfc}/{uuid}", proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(byte[] xmlCancelation)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = RequestCancelarFile(xmlCancelation);
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                "cfdi33/cancel/xml", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(string pfx, string rfc, string password, string uuid)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = RequestCancelar(pfx, rfc, password, uuid);
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                "cfdi33/cancel/pfx", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        public CancelationResponse CancelarByCSD(string cer, string key, string rfc, string password, string uuid)
        {
            return Cancelar(cer, key, rfc, password, uuid);
        }
        public CancelationResponse CancelarByXML(byte[] xmlCancelation)
        {
            return Cancelar(xmlCancelation);
        }
        public CancelationResponse CancelarByPFX(string pfx, string rfc, string password, string uuid)
        {
            return Cancelar(pfx, rfc, password, uuid);
        }
        public CancelationResponse CancelarByRfcUuid(string rfc, string uuid)
        {
            return Cancelar(rfc, uuid);
        }

    }
}
