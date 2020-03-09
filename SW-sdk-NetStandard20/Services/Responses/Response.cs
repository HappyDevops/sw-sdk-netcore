using System.Runtime.Serialization;
using SW.NetStandard20.Exceptions.Asserts;
using SW.NetStandard20.Services.Interfaces;

namespace SW.NetStandard20.Services.Responses
{
    public class Response<T> : IResponseable where  T: new()
    {
        public Response()
        {
            Data = new T();
        }

        public Response(T data)
        {
            ArgsCheck.IsNotNull(nameof(data), data);
            Data = data;
        }

        [DataMember(Name = "Data")]
        public T Data { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "messageDetail")]
        public string MessageDetail { get; set; }


        public static Response<T> GetSuccessfulResponse()
        {
            return new Response<T>();
        }
    }
}