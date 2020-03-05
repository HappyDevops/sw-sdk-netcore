using System.Runtime.Serialization;

namespace SW.NetStandard20.Services.Pendings
{
    public class Data
    {
        [DataMember(Name = "codStatus")]
        public string CodStatus { get; set; }

        [DataMember(Name = "uuid")]
        public string[] UUIDs { get; set; }
    }
}
