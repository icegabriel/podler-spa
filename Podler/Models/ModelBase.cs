using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public class ModelBase
    {
        [DataMember]
        public int Id { get; set; }
    }
}