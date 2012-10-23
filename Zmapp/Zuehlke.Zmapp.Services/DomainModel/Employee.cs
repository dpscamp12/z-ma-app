using System;
using System.Collections.Generic;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.Services.DomainModel
{
	[Serializable]
	public class Employee
	{
		private readonly List<Reservation> reservations = new List<Reservation>();

		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public int ZipCode { get; set; }

		public string Phone { get; set; }

		public string EMail { get; set; }

        public List<Skill> Skills { get; set; }

	    public Employee()
	    {
	        this.Skills = new List<Skill>();
	    }

		public List<Reservation> Reservations
		{
			get { return this.reservations; }
		}

		public CareerLevel CareerLevel { get; set; }

		public bool HasSkill(Skill requestedSkill)
		{
			if (this.Skills == null)
			{
				return false;
			}
			return this.Skills.Any(skill => skill == requestedSkill);
		}

		public bool HasReservation(DateTime date)
		{
			if (this.Reservations == null)
			{
				return false;
			}
			return this.Reservations.Any(reservation => reservation.Contains(date));
		}

		public bool IsAvailable(DateTime date)
		{
			return !this.HasReservation(date);
		}

		public bool HasAnyAvailableTime(DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			for (DateTime date = beginOfPeriod; date <= endOfPeriod; date = date.AddDays(1))
			{
				if (this.IsAvailable(date))
				{
					return true;
				}
			}
			return false;
		}
	}
}
