using System;
using System.Runtime.Serialization;


namespace Zuehlke.Zmapp.Services.Contracts.Employees
{
    [DataContract]
    public class EmployeeQuery
    {
        [DataMember]
        public int CustomerId { get; set; }
        
        [DataMember]
        public Skill[] RequestedSkills { get; set; }

        [DataMember]
        public CareerLevel[] RequestedCareerLevels { get; set; }

        [DataMember]
        public DateTime BeginOfWorkPeriod { get; set; }

        [DataMember]
        public DateTime EndOfWorkPeriod { get; set; }
    }
}