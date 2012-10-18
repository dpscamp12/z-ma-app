
using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.Services.Client
{
	public class EmployeeServiceProxy : ServiceProxy<IEmployeeService>, IEmployeeService
	{
		public EmployeeServiceProxy()
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
		#endregion
	}
}