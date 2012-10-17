using System;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf
{
	public class EmployeeEvaluationServiceProxy : ServiceProxy, IEmployeeEvaluationService
	{
		public EmployeeEvaluationServiceProxy()
			: base("BasicHttpBinding_IEmployeeEvaluationService")
		{
		}

		#region IEmployeeEvaluationService members

		public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
		{
			return this.ExecuteRemoteCall<IEmployeeEvaluationService, EmployeeSearchResult[]>(
				(service) => service.FindPotentialEmployeesForCustomer(query));
		}

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			this.ExecuteRemoteCall<IEmployeeEvaluationService>(
				(service) => service.ReserveEmployeeForCustomer(employeeId, customerId, beginOfPeriod, endOfPeriod));
		}
		#endregion
	}
}