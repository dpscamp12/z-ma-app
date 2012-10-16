using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
    [DataContract]
    public enum CareerLevel
    {
        [EnumMember]
        JuniorSoftwareEngineer = 0,
        [EnumMember]
        SoftwareEngineer = 1
    }
}