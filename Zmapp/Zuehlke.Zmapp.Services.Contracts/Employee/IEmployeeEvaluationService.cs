using System;
using System.ServiceModel;

namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
	[ServiceContract]
	public interface IEmployeeEvaluationService
	{
		[OperationContract]
		EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query);

		[OperationContract]
		void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod);
	}
}