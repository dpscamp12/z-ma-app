using System.Collections.Generic;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.Services.Data
{
	public interface IRepository
	{
		IEnumerable<Employee> GetEmployees();
		Employee GetEmployee(int id);
		void SetEmployee(Employee employee);
		void SetEmployeeBatch(IEnumerable<Employee> employees);
		bool RemoveEmployee(int employeeId);
		IEnumerable<Customer> GetCustomers();
		Customer GetCustomer(int id);
		void SetCustomer(Customer customer);
		void SetCustomerBatch(IEnumerable<Customer> customer);
		bool RemoveCustomer(int customerId);
	}
}