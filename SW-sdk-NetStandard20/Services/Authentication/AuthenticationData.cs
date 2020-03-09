using System.Runtime.Serialization;

namespace SW.NetStandard20.Services.Authentication
{
    public class AuthenticationData
    {
        public AuthenticationData()
        {
            
        }

        public AuthenticationData(string token)
        {
            Token = token;
        }

        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}