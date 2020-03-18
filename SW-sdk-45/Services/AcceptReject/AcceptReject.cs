using System;
using SW.Helpers;
using System.Net;

namespace SW.Services.AcceptReject
{
    public class AcceptReject : AcceptRejectService
    {
        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
       

        AcceptRejectResponseHandler _handler;
        public AcceptReject(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new AcceptRejectResponseHandler();
        }
        public AcceptReject(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new AcceptRejectResponseHandler();
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                var content = RequestAcceptReject(cer, key, rfc, password, uuids);
                return handler.GetPostResponse(Url,
                                "acceptreject/csd", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(byte[] xmlCancelation, EnumAcceptReject enumAcceptReject)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                var content = RequestAcceptReject(xmlCancelation, enumAcceptReject);
                return handler.GetPostResponse(Url,
                                "acceptreject/xml", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                var content = RequestAcceptReject(pfx, rfc, password, uuid);
                return handler.GetPostResponse(Url,
                                "acceptreject/pfx", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = RequestAcceptReject(rfc, uuid, enumAcceptReject);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var headers = GetHeaders();
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url, headers, $"acceptreject/{rfc}/{uuid}/{enumAcceptReject.ToString()}", proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }


        public AcceptRejectResponse AcceptByCSD(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            return AcceptRejectRequest(cer, key, rfc, password, uuids);
        }
        public AcceptRejectResponse AcceptByXML(byte[] xmlCancelation, EnumAcceptReject enumCancelation)
        {
            return AcceptRejectRequest(xmlCancelation, enumCancelation);
        }
        public AcceptRejectResponse AcceptByPFX(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid)
        {
            return AcceptRejectRequest(pfx, rfc, password, uuid);
        }
        public AcceptRejectResponse AcceptByRfcUuid(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            return AcceptRejectRequest(rfc, uuid, enumAcceptReject);
        }
    }
}
