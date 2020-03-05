using System.Runtime.Serialization;
using SW.NetStandard20.Services.Interfaces;

namespace SW.NetStandard20.Services.Pendings
{
    public class PendingsResponse : IResponseable
    {
        [DataMember(Name = "data")]
        public Data Data { get; set; }

        [DataMember(Name = "codStatus")]
        public string CodStatus { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "messageDetail")]
        public string MessageDetail { get; set; }
    }
}