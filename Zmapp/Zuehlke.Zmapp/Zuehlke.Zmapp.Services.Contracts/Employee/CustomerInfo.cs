using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
    [DataContract]
    public class CustomerInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}