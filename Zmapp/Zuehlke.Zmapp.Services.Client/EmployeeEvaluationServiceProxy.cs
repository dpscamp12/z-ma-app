using System;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Client
{
	public class EmployeeEvaluationServiceProxy : ServiceProxy<IEmployeeEvaluationService>, IEmployeeEvaluationService
	{
		public EmployeeEvaluationServiceProxy()
			: base("EmployeeEvaluationService")
		{
		}

		#region IEmployeeEvaluationService members
		public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
		{
			return this.ExecuteRemoteCall((service) => service.FindPotentialEmployeesForCustomer(query));
		}

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			this.ExecuteRemoteCall((service) => service.ReserveEmployeeForCustomer(employeeId, customerId, beginOfPeriod, endOfPeriod));
		}
		#endregion
	}
}