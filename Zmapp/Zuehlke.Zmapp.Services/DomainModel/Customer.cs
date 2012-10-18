using System;

namespace Zuehlke.Zmapp.Services.DomainModel
{
	[Serializable]
	public class Customer
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Street { get; set; }

		public string ZipCode { get; set; }

		public string City { get; set; }
	}
}
