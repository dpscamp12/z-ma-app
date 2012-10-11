using System;

namespace Zuehlke.Zmapp.Services
{
    [Serializable]
    public class Reservation
    {
        public DateTime StartDate { get; set; }

        public DateTime End { get; set; }

        public int? CustomerId { get; set; }
    }
}