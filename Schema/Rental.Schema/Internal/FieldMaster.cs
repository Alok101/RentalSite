using System;
using System.Runtime.Serialization;

namespace Rental.Schema.Internal
{
    [DataContract]
    public class FieldMaster
    {
        [DataMember(Name ="id")]
        public int Id { get; set; }

        [DataMember(Name ="groupCode")]
        public string GroupCode { get; set; }

        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="status")]
        public string Status { get; set; }
        
        [DataMember(Name="createDate")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [DataMember(Name="isUsed")]
        public bool IsUsed { get; set; }

        [DataMember(Name="description")]
        public string Description { get; set; }
    }
}
