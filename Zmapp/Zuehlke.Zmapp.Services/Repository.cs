using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zuehlke.Zmapp.Services
{
    public class Repository
    {
        private static Repository _instance;

        private static IList<Employee> _employees;
        private readonly string EmployeeListFileName;

        private static IList<Customer> _customers;
        private readonly string CustomersListFileName;

        static Repository()
        {
            _instance = new Repository();
        }
        
        public static Repository Instance
        {
            get { return _instance; }
        }

        private Repository()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            EmployeeListFileName = Path.Combine(path, "bin", "Employees.xml");
            CustomersListFileName = Path.Combine(path, "bin", "Customers.xml");      
        }

        public IList<Employee> GetEmployees()
        {
            return Employees;
        }

        public Employee GetEmployee(int id)
        {
            return Employees.FirstOrDefault(e => e.Id == id);
        }

        public bool RemoveEmployee(int employeeId)
        {
            bool isValid = RemoveEmployee(Employees, employeeId);
            if (isValid)
            {
                DataAccess.WriteEmployeesToFile(EmployeeListFileName, Employees);
            }
            return isValid;
        }

        public void SetEmployee(Employee employee)
        {
            if (employee.Id <= 0)
            {
                throw new ArgumentException("Employee ID is invalid.");
            }

            RemoveEmployee(Employees, employee.Id);
            Employees.Add(employee);

            DataAccess.WriteEmployeesToFile(EmployeeListFileName, Employees);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return Customers;
        }

        public Customer GetCustomer(int id)
        {
            return Customers.First(e => e.Id == id);
        }

        public void SetCustomer(Customer customer)
        {
            if (customer.Id <= 0)
            {
                throw new ArgumentException("Customer ID is invalid.");
            }

            RemoveCustomer(Customers, customer.Id);
            Customers.Add(customer);

            DataAccess.WriteCustomersToFile(CustomersListFileName, Customers);
        }

        public bool RemoveCustomer(int customerId)
        {
            bool isValid = RemoveCustomer(Customers, customerId);
            if (isValid)
            {
                DataAccess.WriteCustomersToFile(CustomersListFileName, Customers);
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
            get { return _employees ?? (_employees = DataAccess.ReadEmployeesFromFile(EmployeeListFileName).ToList()); }
        }

        private IList<Customer> Customers
        {
            get { return _customers ?? (_customers = DataAccess.ReadCustomersFromFile(CustomersListFileName).ToList()); }
        }
    }
}
