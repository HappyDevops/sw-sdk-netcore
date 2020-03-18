using SW.Helpers;
using System;
using System.Net;

namespace SW.Services.Relations
{
    public class Relations : RelationsService
    {

        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
       
        RelationsResponseHandler _handler;
        public Relations(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new RelationsResponseHandler();
        }
        public Relations(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new RelationsResponseHandler();
        }
        internal override RelationsResponse RelationsRequest(string cer, string key, string rfc, string password, string uuid)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = RequestRelations(cer, key, rfc, password, uuid);
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                "relations/csd", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override RelationsResponse RelationsRequest(byte[] xmlCancelation)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = RequestRelations(xmlCancelation);
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                "relations/xml", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override RelationsResponse RelationsRequest(string pfx, string rfc, string password, string uuid)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = RequestRelations(pfx, rfc, password, uuid);
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url,
                                "relations/pfx", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override RelationsResponse RelationsRequest(string rfc, string uuid)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = RequestRelations(rfc, uuid);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var headers = GetHeaders();
                var proxy = RequestHelper.ProxySettings(Proxy, ProxyPort);
                return handler.GetPostResponse(Url, headers, $"relations/{rfc}/{uuid}", proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }


        public RelationsResponse RelationsByCSD(string cer, string key, string rfc, string password, string uuid)
        {
            return RelationsRequest(cer, key, rfc, password, uuid);
        }
        public RelationsResponse RelationsByXML(byte[] xmlCancelation)
        {
            return RelationsRequest(xmlCancelation);
        }
        public RelationsResponse RelationsByPFX(string pfx, string rfc, string password, string uuid)
        {
            return RelationsRequest(pfx, rfc, password, uuid);
        }
        public RelationsResponse RelationsByRfcUuid(string rfc, string uuid)
        {
            return RelationsRequest(rfc, uuid);
        }
    }
}
