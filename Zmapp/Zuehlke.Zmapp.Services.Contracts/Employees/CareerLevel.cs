using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employees
{
	[DataContract]
	public enum CareerLevel
	{
		[EnumMember]
		JuniorSoftwareEngineer = 0,
		[EnumMember]
		SoftwareEngineer = 1,
		[EnumMember]
		SeniorSoftwareEngineer = 2,
		[EnumMember]
		LeadSoftwareArchitect = 3,
		[EnumMember]
		PrincipalConsultant = 4,
		[EnumMember]
		ProjectManager = 5
	}
}