﻿using System;
using SW.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services.AcceptReject
{
    public abstract class AcceptRejectService : Services
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
        
        protected AcceptRejectService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected AcceptRejectService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract AcceptRejectResponse AcceptRejectRequest(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuid);
        internal abstract AcceptRejectResponse AcceptRejectRequest(byte[] xmlCancelation, EnumAcceptReject enumCancelation);
        internal abstract AcceptRejectResponse AcceptRejectRequest(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid);
        internal abstract AcceptRejectResponse AcceptRejectRequest(string rfc, string uuid, EnumAcceptReject enumCancelation);
        internal virtual Dictionary<string, string> GetHeaders()
        {
            SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                    { "Authorization", "bearer " + Token }
                };
            return headers;
        }
        internal virtual StringContent RequestAcceptReject(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AcceptRejectRequestCSD
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuids = uuids
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual MultipartFormDataContent RequestAcceptReject(byte[] xmlCancelation, EnumAcceptReject enumAcceptReject)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xmlCancelation);
            content.Add(fileContent, "xml", "xml");
            return content;
        }
        internal virtual StringContent RequestAcceptReject(string pfx, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AcceptRejectRequestPFX
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuids = uuids
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual HttpWebRequest RequestAcceptReject(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            SetupRequest();
            string path = $"acceptreject/{rfc}/{uuid}/{enumAcceptReject.ToString()}";
            var request = (HttpWebRequest)WebRequest.Create(Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + Token);
            RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }

        private void SetupRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}
