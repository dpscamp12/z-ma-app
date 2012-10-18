using System.ServiceModel;

namespace Zuehlke.Zmapp.Services.Contracts.Employees
{
	[ServiceContract]
	public interface IEmployeeService
	{
		[OperationContract]
		EmployeeInfo[] GetEmployees();

		[OperationContract]
		EmployeeInfo GetEmployee(int employeeId);

		[OperationContract]
		void SetEmployee(EmployeeInfo employee);

		[OperationContract]
		bool RemoveEmployee(int employeeId);
	}
}