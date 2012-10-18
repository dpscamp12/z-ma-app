using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.Data;
using Zuehlke.Zmapp.Services.DomainModel;
using Zuehlke.Zmapp.Services.Employees;

namespace Zuehlke.Zmapp.Services.Test.Employees
{
	[TestClass]
	public class EmployeeServiceTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_RepositoryNullReference_ThrowsException()
		{
			new EmployeeService(null);
		}

		[TestMethod]
		public void GetEmployees_ReturnsAllAvailableEmployees()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			// act
			EmployeeInfo[] employees = service.GetEmployees();

			// assert
			Assert.AreEqual(2, employees.Count());
		}

		[TestMethod]
		public void GetEmployee_RequestExistingEmployeeId_ReturnsMatchingEmployees()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			// act
			EmployeeInfo employee = service.GetEmployee(1);

			// assert
			Assert.AreEqual("Roger", employee.FirstName);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void GetEmployee_RequestNotExistingEmployeeId_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			// act
			service.GetEmployee(0);
		}

		[TestMethod]
		public void RemoveEmployee_RequestExistingEmployeeId_RemovesEmployeeInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			// act
			service.RemoveEmployee(1);

			// assert
			mockedRepository.Verify(p => p.RemoveEmployee(1));
		}

		[TestMethod]
		public void SetEmployee_RequestNotExistingEmployee_SetsEmployeeInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			const int employeeId = 23;
			var employeeInfo = new EmployeeInfo
			{
				Id = employeeId
			};

			// act
			service.SetEmployee(employeeInfo);

			// assert
			mockedRepository.Verify(p => p.SetEmployee(It.Is<Employee>(e => e.Id == employeeId)));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SetEmployee_EmployeeNullReference_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			// act
			service.SetEmployee(null);
		}

		[TestMethod]
		public void SetEmployees_RequestNotExistingEmployees_SetsEmployeesInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			const int employeeId1 = 23;
			const int employeeId2 = 24;

			var employeeInfos = new []
			{
				new EmployeeInfo { Id = employeeId1 },
				new EmployeeInfo { Id = employeeId2 },
			};

			// act
			service.SetEmployees(employeeInfos);

			// assert
			mockedRepository.Verify(p => p.SetEmployeeBatch(It.Is<IEnumerable<Employee>>(es => es.Count() == 2)));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SetEmployees_EmployeesNullReference_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeService(mockedRepository.Object);

			// act
			service.SetEmployees(null);
		}

		private static Mock<IRepository> CreateMockedRepository()
		{
			var mock = new Mock<IRepository>();

			IEnumerable<Employee> employees = CreateEmployees();
			mock.Setup(p => p.GetEmployees()).Returns(employees);
			mock.Setup(p => p.GetEmployee(It.IsAny<int>())).Returns<int>((id) => employees.ElementAt(id - 1));
			mock.Setup(p => p.GetEmployee(0)).Returns<int>(null);

			return mock;
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