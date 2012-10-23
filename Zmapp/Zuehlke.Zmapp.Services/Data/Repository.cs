using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.Services.Data
{
	public class Repository : IRepository
	{
        private static readonly string employeeListFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\Data\Employees.xml");
        private static readonly string customersListFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\Data\Customers.xml");
		private static readonly Repository instance;

		private static IList<Employee> employees;
		private static IList<Customer> customers;

		static Repository()
		{
			instance = new Repository();
		}

		public static Repository Instance
		{
			get { return instance; }
		}

		private Repository()
		{
		}

		public IEnumerable<Employee> GetEmployees()
		{
			return this.Employees;
		}

		public Employee GetEmployee(int id)
		{
			return this.Employees.FirstOrDefault(e => e.Id == id);
		}

		public bool RemoveEmployee(int employeeId)
		{
			bool isValid = RemoveEmployee(this.Employees, employeeId);
			if (isValid)
			{
				DataAccess.WriteEmployeesToFile(employeeListFileName, this.Employees);
			}
			return isValid;
		}

		public void SetEmployee(Employee employee)
		{
			if (employee.Id <= 0)
			{
				throw new ArgumentException("Employee ID is invalid.");
			}

			RemoveEmployee(this.Employees, employee.Id);
			this.Employees.Add(employee);

			DataAccess.WriteEmployeesToFile(employeeListFileName, this.Employees);
		}

		public void SetEmployeeBatch(IEnumerable<Employee> employees)
		{
			foreach (var employee in employees)
			{
				if (employee.Id <= 0)
				{
					throw new ArgumentException("Employee ID is invalid.");
				}

				RemoveEmployee(this.Employees, employee.Id);
				this.Employees.Add(employee);
			}

			DataAccess.WriteEmployeesToFile(employeeListFileName, this.Employees);
		}

		public IEnumerable<Customer> GetCustomers()
		{
			return this.Customers;
		}

		public Customer GetCustomer(int id)
		{
			return this.Customers.First(e => e.Id == id);
		}

		public void SetCustomer(Customer customer)
		{
			if (customer.Id <= 0)
			{
				throw new ArgumentException("Customer ID is invalid.");
			}

			RemoveCustomer(this.Customers, customer.Id);
			this.Customers.Add(customer);

			DataAccess.WriteCustomersToFile(customersListFileName, this.Customers);
		}

		public void SetCustomerBatch(IEnumerable<Customer> customers)
		{
			foreach (var customer in customers)
			{
				if (customer.Id <= 0)
				{
					throw new ArgumentException("Customer ID is invalid.");
				}

				RemoveCustomer(this.Customers, customer.Id);
				this.Customers.Add(customer);
			}

			DataAccess.WriteCustomersToFile(customersListFileName, this.Customers);
		}

		public bool RemoveCustomer(int customerId)
		{
			bool isValid = RemoveCustomer(this.Customers, customerId);
			if (isValid)
			{
				DataAccess.WriteCustomersToFile(customersListFileName, this.Customers);
			}
			return isValid;
		}

		private static bool RemoveEmployee(IList<Employee> employees, int employeeId)
		{
			for (int i = employees.Count - 1; i >= 0; i--)
			{
				if (employees[i].Id == employeeId)
				{
					employees.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		private static bool RemoveCustomer(IList<Customer> customers, int customerId)
		{
			for (int i = customers.Count - 1; i >= 0; i--)
			{
				if (customers[i].Id == customerId)
				{
					customers.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		private IList<Employee> Employees
		{
			get { return employees ?? (employees = DataAccess.ReadEmployeesFromFile(employeeListFileName).ToList()); }
		}

		private IList<Customer> Customers
		{
			get { return customers ?? (customers = DataAccess.ReadCustomersFromFile(customersListFileName).ToList()); }
		}
	}
}
