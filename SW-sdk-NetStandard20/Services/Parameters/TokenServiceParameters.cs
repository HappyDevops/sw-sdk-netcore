using System.Collections.Generic;

namespace SW.NetStandard20.Services.Parameters
{
    public class TokenServiceParameters : ISWServiceCredentials
    {
        public bool AreValid => !string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace(EndPoint);

        public TokenServiceParameters()
        {
            ProxySettings = new ProxySettings();
        }
        
        public ProxySettings ProxySettings { get; set; }

        public string EndPoint { get; set; }

        public  string Token { get; set; }

        public Dictionary<string, string> ToAuthenticationHeaders()
        {
            return new Dictionary<string, string>();
        }
    }
}