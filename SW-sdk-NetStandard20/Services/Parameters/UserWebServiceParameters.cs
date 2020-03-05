using System.Collections.Generic;

namespace SW.NetStandard20.Services.Parameters
{
    public class UserWebServiceParameters
    {
        public bool AreValid => !string.IsNullOrWhiteSpace(EndPoint) && !string.IsNullOrWhiteSpace(User) &&
                                !string.IsNullOrWhiteSpace(Password);

        public UserWebServiceParameters()
        {
            ProxySettings = new ProxySettings();
        }
        
        public ProxySettings ProxySettings { get; set; }

        public string EndPoint { get; set; }

        public  string User { get; set; }

        public  string Password { get; set;  }

        public Dictionary<string, string> ToAuthenticationHeaders()
        {
            return new Dictionary<string, string>();
        }
    }
}