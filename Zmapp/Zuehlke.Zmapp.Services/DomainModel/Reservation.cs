using System;

namespace Zuehlke.Zmapp.Services.DomainModel
{
	[Serializable]
	public class Reservation
	{
		private DateTime start;
		private DateTime end;

		public DateTime Start
		{
			get { return this.start; }
			set
			{
				this.start = value.Date;
			}
		}

		public DateTime End
		{
			get { return this.end; }
			set
			{
				this.end = value.Date;
			}
		}

		public int? CustomerId { get; set; }

		public bool Contains(DateTime date)
		{
			return date.Date >= this.Start && date.Date <= this.End;
		}
	}
}