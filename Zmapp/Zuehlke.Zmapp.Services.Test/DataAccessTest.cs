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
        [Ignore]
        //[TestMethod]
        public void CreateTestData()
        {
            const string fileName = "C:/Temp/Employees.xml";
            DataAccess.WriteEmployeesToFile(fileName, CreateEmployeeList());
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
                             Skills = new[] {Skill.CSharp, Skill.SqlServer},
                             CareerLevel = CareerLevel.SoftwareEngineer,
                             Reservations = new[]
                                                {
                                                    new Reservation
                                                        {
                                                            Start = new DateTime(2013, 1, 1),
                                                            End = new DateTime(2013, 2, 5)
                                                        },
                                                    new Reservation
                                                        {
                                                            Start = new DateTime(2013, 1, 1),
                                                            End = new DateTime(2013, 2, 5)
                                                        }
                                                },
                         };

            var e2 = new Employee
                         {
                             Id = 2,
                             FirstName = "Pipilotti",
                             LastName = "Rist",
                             CareerLevel = CareerLevel.SoftwareEngineer
                         };

            return new List<Employee> {e1, e2};
        }
    }
}
