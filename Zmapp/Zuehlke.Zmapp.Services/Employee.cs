using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services
{
    [Serializable]
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Skill[] Skills { get; set; }

        public CareerLevel CareerLevel { get; set; }

        public Reservation[] Reservations { get; set; }

        public bool HasSkill(Skill requestedSkill)
        {
            if (Skills == null)
            {
                return false;
            }
            return Skills.Any(skill => skill == requestedSkill);
        }

        public bool HasReservation(DateTime date)
        {
            if (Reservations == null)
            {
                return false;
            }
            return Reservations.Any(reservation => reservation.Contains(date));
        }

        public bool IsAvailable(DateTime date)
        {
            return !HasReservation(date);
        }

        public bool HasAnyAvailableTime(DateTime beginOfPeriod, DateTime endOfPeriod)
        {
            for (DateTime date = beginOfPeriod; date <= endOfPeriod; date = date.AddDays(1))
            {
                if (IsAvailable(date))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
