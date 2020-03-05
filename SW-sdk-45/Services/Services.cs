using System;
using SW.Helpers;

namespace SW.Services
{
    public   class Services
    {
        public Services()
        {

        }
        public Services(string url, string token, string proxy, int proxyPort)
        {
            ////Url = RequestHelper.NormalizeBaseUrl(url); ;
            ////Token = token;
            ////_expirationDate = DateTime.Now.AddYears(_timeSession);
            ////Proxy = proxy;
            ////ProxyPort = proxyPort;
        }
        public Services(string url, string user, string password, string proxy, int proxyPort)
        {
            //Url = RequestHelper.NormalizeBaseUrl(url); ;
            //User = user;
            //Password = password;
            //Proxy = proxy;
            //ProxyPort = proxyPort;
        }
    }
}
