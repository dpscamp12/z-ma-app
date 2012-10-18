using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employees
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