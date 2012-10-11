using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
    [DataContract]
    public class EmployeeSearchResult
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public Skill[] Skills { get; set; }

        [DataMember]
        public CareerLevel Level { get; set; }

        [DataMember]
        public float Distance { get; set; }
    }
}