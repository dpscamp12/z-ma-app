using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Zuehlke.Zmapp.Services
{
    public static class DataAccess
    {
        public static void WriteEmployeesToFile(string fileName, IEnumerable<Employee> employees)
        {
            var serializer = new XmlSerializer(typeof(EmployeeList));
            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, new EmployeeList {Employees = employees.ToArray()});
            }
        }

        public static IEnumerable<Employee> ReadEmployeesFromFile(string fileName)
        {
            var serializer = new XmlSerializer(typeof(EmployeeList));
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var recipe = (EmployeeList)serializer.Deserialize(fs);
                return recipe.Employees;
            }
        }

        public static void WriteCustomersToFile(string fileName, IEnumerable<Customer> customers)
        {
            var serializer = new XmlSerializer(typeof(CustomerList));
            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, new CustomerList() { Customers = customers.ToArray() });
            }
        }

        public static IEnumerable<Customer> ReadCustomersFromFile(string fileName)
        {
            var serializer = new XmlSerializer(typeof(CustomerList));
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var recipe = (CustomerList)serializer.Deserialize(fs);
                return recipe.Customers;
            }
        }

        [Serializable]
        [XmlRoot("EmployeeList")]
        public class EmployeeList
        {
            public Employee[] Employees { get; set; }
        }

        [Serializable]
        [XmlRoot("CustomerList")]
        public class CustomerList
        {
            public Customer[] Customers { get; set; }
        }
    }
}