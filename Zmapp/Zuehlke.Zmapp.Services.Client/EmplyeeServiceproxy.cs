using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Client
{
	public class EmplyeeServiceProxy : ServiceProxy<IEmployeeService>, IEmployeeService
	{
		protected EmplyeeServiceProxy()
			: base("EmployeeService")
		{
		}

		#region Implementation of IEmployeeService
		public EmployeeInfo[] GetEmployees()
		{
			return this.ExecuteRemoteCall((service) => service.GetEmployees());
		}

		public EmployeeInfo GetEmployee(int employeeId)
		{
			return this.ExecuteRemoteCall((service) => service.GetEmployee(employeeId));
		}

		public void SetEmployee(EmployeeInfo employee)
		{
			this.ExecuteRemoteCall((service) => service.SetEmployee(employee));
		}

		public bool RemoveEmployee(int employeeId)
		{
			return this.ExecuteRemoteCall((service) => service.RemoveEmployee(employeeId));
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