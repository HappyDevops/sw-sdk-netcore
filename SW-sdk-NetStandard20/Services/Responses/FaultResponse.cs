using System.Runtime.Serialization;
using SW.NetStandard20.Services.Interfaces;

namespace SW.NetStandard20.Services.Responses
{
    public class FaultResponse : IResponseable
    {
        public FaultResponse()
        {
            Status = "error";
        }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "messageDetail")]
        public string MessageDetail { get; set; }
    }
}