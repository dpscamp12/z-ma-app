using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zuehlke.Zmapp.Services;
using Zuehlke.Zmapp.Services.Contracts.Employee;

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

    public class ReservationViewModel
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string CustomerName { get; set; }
    }
}