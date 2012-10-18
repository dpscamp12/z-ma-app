using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employees
{
	[DataContract]
	public class EmployeeInfo
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string FirstName { get; set; }

		[DataMember]
		public string LastName { get; set; }

		[DataMember]
		public string Street { get; set; }

		[DataMember]
		public int ZipCode { get; set; }

		[DataMember]
		public string City { get; set; }

		[DataMember]
		public string Phone { get; set; }

		[DataMember]
		public string EMail { get; set; }

		[DataMember]
		public CareerLevel CareerLevel { get; set; }

		[DataMember]
		public Skill[] Skills { get; set; }
	}
}