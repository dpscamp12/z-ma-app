using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employees
{
	[DataContract]
	public enum Skill
	{
		[EnumMember]
		Windows7 = 0,
		[EnumMember]
		CSharp = 1,
		[EnumMember]
		SqlServer = 2,
		[EnumMember]
		AspDotNet = 3,
		[EnumMember]
		CloudComputing = 4,
		[EnumMember]
		WPF = 5,
		[EnumMember]
		WCF = 6
	}
}