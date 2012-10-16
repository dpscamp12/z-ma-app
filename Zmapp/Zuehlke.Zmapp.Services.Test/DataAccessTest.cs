using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Test
{
    [TestClass]
    public class DataAccessTest
    {
       // [Ignore]
        [TestMethod]
        public void CreateTestData()
        {
            DataAccess.WriteEmployeesToFile("C:/Temp/Employees.xml", CreateEmployeeList());
            DataAccess.WriteCustomersToFile("C:/Temp/Customers.xml", CreateCustomerList());
        }

        [TestMethod]
        public void WriteAndReadEmployees()
        {
            string fileName = Path.GetTempFileName();

            try
            {
                List<Employee> initialEmployees = CreateEmployeeList();
                DataAccess.WriteEmployeesToFile(fileName, initialEmployees);

                List<Employee> restoredEmployees = DataAccess.ReadEmployeesFromFile(fileName).ToList();
                Assert.AreEqual(initialEmployees.Count, restoredEmployees.Count);
            }
            finally
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }

        private static List<Employee> CreateEmployeeList()
        {
            var e1 = new Employee
                         {
                             Id = 1,
                             FirstName = "Roger",
                             LastName = "Federerr",
                             Street = "Schaffhauserstrasse 23",
                             City = "Zürich",
                             ZipCode = 8006,
                             Phone = "+41 44 999 99 99",
                             EMail = "dspcamp12@gmail.com",
                             Skills = new[] {Skill.CSharp, Skill.SqlServer},
                             CareerLevel = CareerLevel.SoftwareEngineer,
                             Reservations = new[]
                                                {
                                                    new Reservation
                                                        {
                                                            Start = new DateTime(2013, 1, 1),
                                                            End = new DateTime(2013, 2, 5),
                                                            CustomerId = 1
                                                        },
                                                    new Reservation
                                                        {
                                                            Start = new DateTime(2013, 1, 1),
                                                            End = new DateTime(2013, 2, 5),
                                                            CustomerId = 2
                                                            
                                                        }
                                                },
                         };

            var e2 = new Employee
                         {
                             Id = 2,
                             FirstName = "Pipilotti",
                             LastName = "Rist",
                             Street = "Wiesenstrasse 10a",
                             City = "Schlieren",
                             ZipCode = 8952,
                             Phone = "+41 44 999 99 99",
                             EMail = "dspcamp12@gmail.com",
                             CareerLevel = CareerLevel.SoftwareEngineer
                         };

            return new List<Employee> {e1, e2};
        }

        private static List<Customer> CreateCustomerList()
        {
            return new List<Customer>
                       {
                           new Customer() {Id = 1, Name = "Phonaxis"},
                           new Customer() {Id = 2, Name = "BAA"},
                           new Customer() {Id = 3, Name = "Vereinigte Bank"}
                       };
        }
    }
}
