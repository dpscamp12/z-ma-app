using System;

namespace Zuehlke.Zmapp.Services
{
	[Serializable]
	internal class Reservation
	{
		private DateTime _start;
		private DateTime _end;

		public DateTime Start
		{
			get { return _start; }
			set
			{
				_start = value.Date;
			}
		}

		public DateTime End
		{
			get { return _end; }
			set
			{
				_end = value.Date;
			}
		}

		public int? CustomerId { get; set; }

		public bool Contains(DateTime date)
		{
			return date.Date >= Start && date.Date <= End;
		}
	}
}