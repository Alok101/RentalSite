using System.Runtime.Serialization;

namespace Rental.Schema.Common
{
    [DataContract]
    public class Message<T>
    {
        [DataMember(Name = "version")]
        public string Version { get; set; }

        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }

        [DataMember(Name = "isError")]
        public string IsError { get; set; }

        [DataMember(Name = "result")]
        public T Data { get; set; }
    }
}
