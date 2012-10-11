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

        public IEnumerable<Employee> GetEmployees()
        {
            if (_employees == null)
            {
                _employees = DataAccess.ReadEmployeesFromFile(EmployeeListFileName).ToList();
            }

            return _employees;
        }

        public Employee GetEmployee(int id)
        {
            return GetEmployees().FirstOrDefault(e => e.Id == id);
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
    }
}
