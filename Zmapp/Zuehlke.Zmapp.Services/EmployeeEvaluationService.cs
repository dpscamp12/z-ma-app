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
		private readonly IRepository repository = Repository.Instance;

		public EmployeeEvaluationService()
		{
		}

		public EmployeeEvaluationService(IRepository repository)
		{
			if (repository == null) throw new ArgumentNullException("repository");

			this.repository = repository;
		}

		public CustomerInfo[] GetCustomers()
		{
			return Repository.Instance.GetCustomers()
				.Select(c => new CustomerInfo { Id = c.Id, Name = c.Name, Street = c.Street, City = c.City, ZipCode = c.ZipCode })
				.ToArray();
		}

		public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
		{
			IEnumerable<Employee> foundEmployees = this.FindEmployees(query);
			return foundEmployees
				.Select(e => new EmployeeSearchResult
								 {
									 Id = e.Id,
									 Distance = 10.1F,
									 EmployeeName = String.Format("{0} {1}", e.FirstName, e.LastName),
									 Skills = e.Skills.ToArray(),
									 Level = e.CareerLevel
								 })
				.ToArray();
		}

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			var employee = this.repository.GetEmployee(employeeId);

			if (employee == null)
			{
				var error = string.Format("Employee with Id '{0}' not found.", employeeId);
				throw new ArgumentException(error, "employeeId");
			}

			employee.Reservations.Add(new Reservation() { CustomerId = customerId, Start = beginOfPeriod, End = endOfPeriod });

			this.repository.SetEmployee(employee);
		}

		public IEnumerable<Employee> FindEmployees(EmployeeQuery query)
		{
			return FindEmployees(this.repository.GetEmployees(), query);
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
