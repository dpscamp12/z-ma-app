using System;
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
    }
}
