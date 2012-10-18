using System.Collections.Generic;

namespace Zuehlke.Zmapp.Services
{
	public interface IRepository
	{
		IList<Employee> GetEmployees();
		Employee GetEmployee(int id);
		void SetEmployee(Employee employee);
		bool RemoveEmployee(int employeeId);
		IEnumerable<Customer> GetCustomers();
		Customer GetCustomer(int id);
		void SetCustomer(Customer customer);
		void SetCustomerBatch(IEnumerable<Customer> customer);
		bool RemoveCustomer(int customerId);
	}
}