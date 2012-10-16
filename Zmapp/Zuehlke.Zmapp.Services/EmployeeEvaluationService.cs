using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EmployeeEvaluationService : IEmployeeEvaluationService
    {
        public CustomerInfo[] GetCustomers()
        {
            return Repository.Instance.GetCustomers()
                .Select(c => new CustomerInfo { Id = c.Id, Name = c.Name })
                .ToArray();
        }

        public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
        {
            IEnumerable<Employee> foundEmployees = FindEmployees(query);
            return foundEmployees
                .Select(e => new EmployeeSearchResult
                                 {
                                     Id = e.Id,
                                     Distance = 10.1F,
                                     EmployeeName = String.Format("{0} {1}", e.FirstName, e.LastName),
                                     Skills = e.Skills,
                                     Level = e.CareerLevel
                                 })
                .ToArray();
        }

	    public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
	    {
		    throw new NotImplementedException();
	    }

	    public IEnumerable<Employee> FindEmployees(EmployeeQuery query)
        {
            return FindEmployees(Repository.Instance.GetEmployees(), query);
        }

        public static IEnumerable<Employee> FindEmployees(IEnumerable<Employee> employees, EmployeeQuery query)
        {
            var foundEmployees = new List<Employee>();

            foreach (Employee employee in employees)
            {
                // skills
                if (query.RequestedSkills != null && query.RequestedSkills.Length > 0 &&
                    !query.RequestedSkills.Any(employee.HasSkill))
                {
                    continue;
                }

                // career level
                if (query.RequestedCareerLevels != null && query.RequestedCareerLevels.Length > 0 &&
                    !query.RequestedCareerLevels.Any(requestedCareerLevel => employee.CareerLevel == requestedCareerLevel))
                {
                    continue;
                }

                // free time
                if (!employee.HasAnyAvailableTime(query.BeginOfWorkPeriod, query.EndOfWorkPeriod))
                {
                    continue;
                }

                foundEmployees.Add(employee);
            }

            return foundEmployees;
        }
    }
}
