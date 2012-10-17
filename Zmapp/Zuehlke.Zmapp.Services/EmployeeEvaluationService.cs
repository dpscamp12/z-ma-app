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
		private readonly IRepository repository;

		public EmployeeEvaluationService()
			: this(Repository.Instance)
		{
		}

		internal EmployeeEvaluationService(IRepository repository)
		{
			if (repository == null) throw new ArgumentNullException("repository");

			this.repository = repository;
		}

		#region IEmployeeEvaluationService members

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
		#endregion

		private IEnumerable<Employee> FindEmployees(EmployeeQuery query)
		{
			return EmployeeEvaluationServiceHelper.FindEmployees(this.repository.GetEmployees(), query);
		}
	}
}
