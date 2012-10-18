using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.WebApp.Models
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public int ZipCode { get; set; }

		public string Phone { get; set; }

		public string EMail { get; set; }

		public Skill[] Skills { get; set; }

		public CareerLevel CareerLevel { get; set; }

		public ReservationViewModel[] Reservations { get; set; }
	}
}