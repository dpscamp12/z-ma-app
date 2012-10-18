using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.Data;
using Zuehlke.Zmapp.Services.DomainModel;
using Zuehlke.Zmapp.Services.Employees;

namespace Zuehlke.Zmapp.Services.Test.Employees
{
	[TestClass]
	public class EmployeeReservationServiceTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_RepositoryNullReference_ThrowsException()
		{
			new EmployeeReservationService(null);
		}

		[TestMethod]
		public void GetReservationsOfEmployee_RequestsExistingEmployeeId_ReturnsAllReservationsOfRequestedId()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			// act
			ReservationInfo[] reservations = service.GetReservationsOfEmployee(1);

			// assert
			Assert.AreEqual(2, reservations.Count());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void GetReservationsOfEmployee_RequestsNotExistingEmployeeId_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			// act
			service.GetReservationsOfEmployee(0);
		}

		[TestMethod]
		public void SetReservationsOfEmployee_RequestsExistingEmployeeId_SetsEmployeeWithReservationsInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			var reservations = new[]
			{
				new ReservationInfo { CustomerId = 1, Start = new DateTime(2013, 1, 1), End = new DateTime(2013, 1, 2) }
			};

			// act
			service.SetReservationsOfEmployee(1, reservations);

			// assert
			mockedRepository.Verify(p => p.SetEmployee(It.Is<Employee>(e => e.Reservations.Count() == 1)));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SetReservationsOfEmployee_RequestsNotExistingEmployeeId_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			var reservations = new[]
			{
				new ReservationInfo { CustomerId = 1, Start = new DateTime(2013, 1, 1), End = new DateTime(2013, 1, 2) }
			};

			// act
			service.SetReservationsOfEmployee(0, reservations);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SetReservationsOfEmployee_ReservationsNullReference_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			// act
			service.SetReservationsOfEmployee(1, null);
		}

		[TestMethod]
		public void ReserveEmployeeForCustomer_ExistingEmployeeAndExistingCustomer_AddsReservationInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			// act
			service.ReserveEmployeeForCustomer(1, 2, new DateTime(2013, 1, 1), new DateTime(2013, 1, 2));

			// assert
			mockedRepository.Verify(p => p.SetEmployee(It.Is<Employee>(e => e.Reservations.Count() == 3)));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReserveEmployeeForCustomer_NotExistingEmployeeAndExistingCustomer_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			// act
			service.ReserveEmployeeForCustomer(0, 2, new DateTime(2013, 1, 1), new DateTime(2013, 1, 2));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void FindPotentialEmployeesForCustomer_QueryNullReference_ThrowsException()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			// act
			service.FindPotentialEmployeesForCustomer(null);
		}

		[TestMethod]
		public void FindPotentialEmployeesForCustomer_NoRequestedSkillsAndCareerlevels_ReturnsAllEmployees()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new EmployeeReservationService(mockedRepository.Object);

			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = new DateTime(2013, 1, 1),
				EndOfWorkPeriod = new DateTime(2015, 1, 4),
			};

			// act
			EmployeeSearchResult[] result = service.FindPotentialEmployeesForCustomer(query);

			// assert
			Assert.AreEqual(2, result.Count());
		}

		private Mock<IRepository> CreateMockedRepository()
		{
			var mock = new Mock<IRepository>();

			IEnumerable<Customer> customers = CreateCustomers();
			mock.Setup(p => p.GetCustomers()).Returns(customers);

			IEnumerable<Employee> employees = CreateEmployees();
			mock.Setup(p => p.GetEmployees()).Returns(employees);
			mock.Setup(p => p.GetEmployee(It.IsAny<int>())).Returns<int>((id) => employees.ElementAt(id - 1));
			mock.Setup(p => p.GetEmployee(0)).Returns<int>(null);

			return mock;
		}

		private IEnumerable<Customer> CreateCustomers()
		{
			var c1 = new Customer
			{
				Id = 1,
				Name = "UBS",
				Street = "Langstrasse 21",
				ZipCode = "8000",
				City = "Zürich"
			};

			var c2 = new Customer
			{
				Id = 2,
				Name = "Google",
				Street = "Sauberstrasse 1001",
				ZipCode = "8234",
				City = "Niederbip"
			};

			var customers = new[] { c1, c2 };
			return customers;
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
				new Reservation {CustomerId = 1, Start = new DateTime(2013, 1, 1), End = new DateTime(2013, 2, 1)},
				new Reservation {CustomerId = 2, Start = new DateTime(2013, 3, 1), End = new DateTime(2013, 4, 1)},
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
				new Reservation { CustomerId = 1, Start = new DateTime(2013, 1, 1), End = new DateTime(2014, 1, 1) }
			});

			var employees = new[] { e1, e2 };
			return employees;
		}
	}
}