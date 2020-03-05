using System.Runtime.Serialization;
using SW.NetStandard20.Services.Interfaces;

namespace SW.NetStandard20.Services.Authentication
{
    public class AuthenticationResponse : IResponseable
    {
        [DataMember(Name = "Data")]
        public Data Data { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "messageDetail")]
        public string MessageDetail { get; set; }
    }
}
