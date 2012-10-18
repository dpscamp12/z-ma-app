using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.Services.Embloyee;

namespace Zuehlke.Zmapp.Services.Test
{
	[TestClass]
	public class EmployeeReservationServiceHelperTests
	{
		[TestMethod]
		public void FindEmployeesTest()
		{
			var e1 = new Employee
			{
				Id = 1,
				FirstName = "Roger",
				LastName = "Federerr",
				CareerLevel = CareerLevel.SoftwareEngineer,
			};

			e1.Skills.AddRange(new[] { Skill.CSharp, Skill.SqlServer });
			e1.Reservations.AddRange(new[] {
						new Reservation { Start = new DateTime(2013, 1, 1), End = new DateTime(2013, 2, 1) },
						new Reservation { Start = new DateTime(2013, 3, 1), End = new DateTime(2013, 4, 1) },
					});

			var e2 = new Employee
			{
				Id = 2,
				FirstName = "Pipilotti",
				LastName = "Rist",
				CareerLevel = CareerLevel.JuniorSoftwareEngineer,
			};

			e1.Skills.AddRange(new[] { Skill.CSharp, Skill.WCF });
			e1.Reservations.Add(new Reservation { Start = new DateTime(2013, 1, 1), End = new DateTime(2014, 1, 1) });

			var employees = new[] { e1, e2 };

			var query = new EmployeeQuery
							{
								BeginOfWorkPeriod = new DateTime(2013, 1, 1),
								EndOfWorkPeriod = new DateTime(2015, 1, 4),
							};

			// no requested skill/careerLevel
			Assert.IsTrue(EmployeeReservationServiceHelper.FindEmployees(employees, query).Count() == 2);

			// set skill
			query.RequestedSkills = new[] { Skill.SqlServer };
			Assert.IsTrue(EmployeeReservationServiceHelper.FindEmployees(employees, query).Count() == 1);

			query.RequestedSkills = new[] { Skill.SqlServer, Skill.CSharp, };
			Assert.IsTrue(EmployeeReservationServiceHelper.FindEmployees(employees, query).Count() == 2);

			// set career-level
			query.RequestedCareerLevels = new[] { CareerLevel.JuniorSoftwareEngineer };
			Assert.IsTrue(EmployeeReservationServiceHelper.FindEmployees(employees, query).Count() == 1);

			query.RequestedCareerLevels = new[] { CareerLevel.JuniorSoftwareEngineer, CareerLevel.SoftwareEngineer, };
			Assert.IsTrue(EmployeeReservationServiceHelper.FindEmployees(employees, query).Count() == 2);
		}

	}
}