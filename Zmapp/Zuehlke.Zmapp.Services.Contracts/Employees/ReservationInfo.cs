using System;
using System.Runtime.Serialization;

namespace Zuehlke.Zmapp.Services.Contracts.Employees
{
	[DataContract]
	public class ReservationInfo
	{
		[DataMember]
		public DateTime Start { get; set; }

		[DataMember]
		public DateTime End { get; set; }

		[DataMember]
		public int? CustomerId { get; set; }
	}
}