using System;
using SW.Entities;
using SW.Helpers;

namespace SW.Services.Account
{
    public abstract class BalanceAccountService : Services
    {
        private DateTime _expirationDate;
        private int _timeSession = 2;

        protected BalanceAccountService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected BalanceAccountService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }

        public string Token { get; private set; }
        public string Url { get; }
        public string User { get; }
        public string Password { get; }
        public string Proxy { get; }
        public int ProxyPort { get; }
        public DateTime ExpirationDate => _expirationDate;
        internal abstract Response GetBalance();

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
