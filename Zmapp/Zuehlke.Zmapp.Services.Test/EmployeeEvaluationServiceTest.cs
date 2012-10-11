using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Test
{
    [TestClass]
    public class EmployeeEvaluationServiceTest
    {
        [TestMethod]
        public void FindEmployeesTest()
        {
            var e1 = new Employee
            {
                Id = 1,
                FirstName = "Roger",
                LastName = "Federerr",
                Skills = new[] { Skill.CSharp, Skill.SqlServer },
                CareerLevel = CareerLevel.SoftwareEngineer,
                Reservations = new[]
                    {
                        new Reservation { Start = new DateTime(2013, 1, 1), End = new DateTime(2013, 2, 1) },
                        new Reservation { Start = new DateTime(2013, 3, 1), End = new DateTime(2013, 4, 1) },
                    },
                                        
            };

            var e2 = new Employee
            {
                Id = 2,
                FirstName = "Pipilotti",
                LastName = "Rist",
                Skills = new[] { Skill.CSharp, Skill.WCF },
                CareerLevel = CareerLevel.JuniorSoftwareEngineer,
                Reservations = new[] {
                    new Reservation { Start = new DateTime(2013, 1, 1), End = new DateTime(2014, 1, 1) },
                }
            };

            var employees = new[] {e1, e2};

            var query = new EmployeeQuery
                            {
                                BeginOfWorkPeriod = new DateTime(2013, 1, 1),
                                EndOfWorkPeriod = new DateTime(2015, 1, 4),
                            };

            // no requested skill/careerLevel
            Assert.IsTrue(EmployeeEvaluationService.FindEmployees(employees, query).Count() == 2);

            // set skill
            query.RequestedSkills = new[] {Skill.SqlServer};
            Assert.IsTrue(EmployeeEvaluationService.FindEmployees(employees, query).Count() == 1);

            query.RequestedSkills = new[] { Skill.SqlServer, Skill.CSharp,  };
            Assert.IsTrue(EmployeeEvaluationService.FindEmployees(employees, query).Count() == 2);

            // set career-level
            query.RequestedCareerLevels = new[] { CareerLevel.JuniorSoftwareEngineer };
            Assert.IsTrue(EmployeeEvaluationService.FindEmployees(employees, query).Count() == 1);

            query.RequestedCareerLevels = new[] { CareerLevel.JuniorSoftwareEngineer, CareerLevel.SoftwareEngineer,  };
            Assert.IsTrue(EmployeeEvaluationService.FindEmployees(employees, query).Count() == 2);
        }

    }
}