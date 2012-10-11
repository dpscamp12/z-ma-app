using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services
{
    public class EmployeeEvaluationService : IEmployeeEvaluationService
    {
        public CustomerInfo[] GetCustomers()
        {
            return new[] { new CustomerInfo { Id = 1, Name = "DummyName" } };
        }

        public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(int customerId, Skill[] skills, CareerLevel[] level, DateTime from, DateTime though)
        {
            return new[] { new EmployeeSearchResult { Distance = 10.1F, EmployeeName = "Dummy, CrashTest", Skills = new[] { Skill.SqlServer, Skill.CSharp } } };
        }
    }
}
