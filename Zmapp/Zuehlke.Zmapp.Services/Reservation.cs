using System;

namespace Zuehlke.Zmapp.Services
{
    [Serializable]
    public class Reservation
    {
        private DateTime _start;
        private DateTime _end;
        
        public DateTime Start
        {
            get { return _start; }
            set
            {
                _start = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0 );
            }
        }

        public DateTime End
        {
            get { return _end; }
            set
            {
                _end = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999 );
            }
        }

        public int? CustomerId { get; set; }

        public bool Contains(DateTime date)
        {
            return date >= Start && date <= End;
        }
    }
}