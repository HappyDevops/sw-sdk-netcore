using System.Runtime.Serialization;

namespace SW.NetStandard20.Services.Authentication
{
    public class Data
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}