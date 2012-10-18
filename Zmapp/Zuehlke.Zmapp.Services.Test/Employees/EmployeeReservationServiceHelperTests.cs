using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.DomainModel;
using Zuehlke.Zmapp.Services.Employees;

namespace Zuehlke.Zmapp.Services.Test.Employees
{
	[TestClass]
	public class EmployeeReservationServiceHelperTests
	{
		[TestMethod]
		public void FindEmployees_NoRequestedSkillsAndCareerlevels_ReturnsAllEmployees()
		{
			// arrange
			IEnumerable<Employee> employees = CreateEmployees();

			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = new DateTime(2013, 1, 1),
				EndOfWorkPeriod = new DateTime(2015, 1, 4),
			};

			// act
			IEnumerable<Employee> result = EmployeeReservationServiceHelper.FindEmployees(employees, query);

			// assert
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void FindEmployees_RequestASkillFromOneEmployee_ReturnsOnlyMatchingEmployees()
		{
			// arrange
			IEnumerable<Employee> employees = CreateEmployees();

			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = new DateTime(2013, 1, 1),
				EndOfWorkPeriod = new DateTime(2015, 1, 4),
				RequestedSkills = new[] { Skill.SqlServer }
			};

			// act
			IEnumerable<Employee> result = EmployeeReservationServiceHelper.FindEmployees(employees, query);

			// assert
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void FindEmployees_RequestSkillsFromAllEmployee_ReturnsAllEmployees()
		{
			// arrange
			IEnumerable<Employee> employees = CreateEmployees();

			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = new DateTime(2013, 1, 1),
				EndOfWorkPeriod = new DateTime(2015, 1, 4),
				RequestedSkills = new[] { Skill.SqlServer, Skill.CSharp }
			};

			// act
			IEnumerable<Employee> result = EmployeeReservationServiceHelper.FindEmployees(employees, query);

			// assert
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void FindEmployees_RequestOneCareerlevels_ReturnsOnlyMatchingEmployees()
		{
			// arrange
			IEnumerable<Employee> employees = CreateEmployees();

			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = new DateTime(2013, 1, 1),
				EndOfWorkPeriod = new DateTime(2015, 1, 4),
				RequestedCareerLevels = new[] { CareerLevel.JuniorSoftwareEngineer },
			};

			// act
			IEnumerable<Employee> result = EmployeeReservationServiceHelper.FindEmployees(employees, query);

			// assert
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void FindEmployees_RequestAllCareerlevels_ReturnsAllEmployees()
		{
			// arrange
			IEnumerable<Employee> employees = CreateEmployees();

			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = new DateTime(2013, 1, 1),
				EndOfWorkPeriod = new DateTime(2015, 1, 4),
				RequestedCareerLevels = new[] { CareerLevel.JuniorSoftwareEngineer, CareerLevel.SoftwareEngineer },
			};

			// act
			IEnumerable<Employee> result = EmployeeReservationServiceHelper.FindEmployees(employees, query);

			// assert
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void FindEmployees_RequestedPeriodContainsOnlyOneEmployee_ReturnsOnlyMatchingEmployees()
		{
			// arrange
			IEnumerable<Employee> employees = CreateEmployees();

			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = new DateTime(2013, 5, 1),
				EndOfWorkPeriod = new DateTime(2013, 6, 1),
			};

			// act
			IEnumerable<Employee> result = EmployeeReservationServiceHelper.FindEmployees(employees, query);

			// assert
			Assert.AreEqual(1, result.Count());
		}

		private static IEnumerable<Employee> CreateEmployees()
		{
			var e1 = new Employee
			{
				Id = 1,
				FirstName = "Roger",
				LastName = "Federerr",
				CareerLevel = CareerLevel.SoftwareEngineer,
			};

			e1.Skills.AddRange(new[] { Skill.CSharp, Skill.SqlServer });
			e1.Reservations.AddRange(new[]
			{
				new Reservation {Start = new DateTime(2013, 1, 1), End = new DateTime(2013, 2, 1)},
				new Reservation {Start = new DateTime(2013, 3, 1), End = new DateTime(2013, 4, 1)},
			});

			var e2 = new Employee
			{
				Id = 2,
				FirstName = "Pipilotti",
				LastName = "Rist",
				CareerLevel = CareerLevel.JuniorSoftwareEngineer,
			};

			e2.Skills.AddRange(new[] { Skill.CSharp, Skill.WCF });
			e2.Reservations.AddRange(new[]
			{
				new Reservation { Start = new DateTime(2013, 1, 1), End = new DateTime(2014, 1, 1) }
			});

			var employees = new[] { e1, e2 };
			return employees;
		}
	}
}