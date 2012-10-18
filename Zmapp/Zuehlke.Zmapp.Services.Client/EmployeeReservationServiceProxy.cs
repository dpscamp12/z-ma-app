using System;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Client
{
	public class EmployeeReservationServiceProxy : ServiceProxy<IEmployeeReservationService>, IEmployeeReservationService
	{
		public EmployeeReservationServiceProxy()
			: base("EmployeeReservationService")
		{
		}

		#region IEmployeeReservationService members
		public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
		{
			return this.ExecuteRemoteCall((service) => service.FindPotentialEmployeesForCustomer(query));
		}

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			this.ExecuteRemoteCall((service) => service.ReserveEmployeeForCustomer(employeeId, customerId, beginOfPeriod, endOfPeriod));
		}

		public ReservationInfo[] GetReservationsOfEmployee(int employeeId)
		{
			return this.ExecuteRemoteCall((service) => service.GetReservationsOfEmployee(employeeId));
		}

		public void SetReservationsOfEmployee(int employeeId, ReservationInfo[] reservations)
		{
			this.ExecuteRemoteCall((service) => service.SetReservationsOfEmployee(employeeId, reservations));
		}
		#endregion
	}
}