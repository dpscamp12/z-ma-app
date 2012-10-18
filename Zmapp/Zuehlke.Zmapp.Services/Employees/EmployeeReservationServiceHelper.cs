using System.Collections.Generic;
using System.Linq;

using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.Services.Employees
{
	internal static class EmployeeReservationServiceHelper
	{
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