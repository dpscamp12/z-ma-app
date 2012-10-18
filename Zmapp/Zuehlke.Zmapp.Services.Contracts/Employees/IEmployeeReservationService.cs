using System;
using System.ServiceModel;

namespace Zuehlke.Zmapp.Services.Contracts.Employees
{
	[ServiceContract]
	public interface IEmployeeReservationService
	{
		[OperationContract]
		EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query);

		[OperationContract]
		void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod);

		[OperationContract]
		ReservationInfo[] GetReservationsOfEmployee(int employeeId);

		[OperationContract]
		void SetReservationsOfEmployee(int employeeId, ReservationInfo[] reservations);
	}
}