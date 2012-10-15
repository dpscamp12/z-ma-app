using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuehlke.Zmapp.Services
{
    public class Repository
    {
        private static Repository _instance;

        private static IList<Employee> _employees;
        private const string EmployeeListFileName = "C:/Temp/Employees.xml";

        private static IList<Customer> _customers;
        private const string CustomersListFileName = "C:/Temp/Customers.xml";

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
            if (_customers == null)
            {
                _customers = DataAccess.ReadCustomersFromFile(CustomersListFileName).ToList();
            }

            return _customers;
        }

        public Customer GetCustomer(int id)
        {
            return GetCustomers().First(e => e.Id == id);
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

        private IList<Employee> Employees
        {
            get { return _employees ?? (_employees = DataAccess.ReadEmployeesFromFile(EmployeeListFileName).ToList()); }
        }
    }
}
