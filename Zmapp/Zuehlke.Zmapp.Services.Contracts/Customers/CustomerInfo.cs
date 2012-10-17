using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Customers
{
    [DataContract]
    public class CustomerInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string City { get; set; }
    }
}