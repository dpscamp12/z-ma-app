using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Customers;
using Zuehlke.Zmapp.Services.Data;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.Services.Test.Customers
{
	[TestClass]
	public class CustomerServiceTests
	{
		[TestMethod]
		public void GetCustomers_ReturnsAllAvailableCustomers()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new CustomerService(mockedRepository.Object);

			// act
			CustomerInfo[] customers = service.GetCustomers();

			// assert
			Assert.AreEqual(2, customers.Count());
		}

		[TestMethod]
		public void RemoveCustomer_RequestExistingCustomerId_RemovesCustomerInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new CustomerService(mockedRepository.Object);

			// act
			service.RemoveCustomer(1);

			// assert
			mockedRepository.Verify(p => p.RemoveCustomer(1));
		}

		[TestMethod]
		public void SetCustomer_RequestNotExistingCustomer_SetsCustomerInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new CustomerService(mockedRepository.Object);

			const int customerId = 23;
			var customerInfo = new CustomerInfo
			{
				Id = customerId
			};

			// act
			service.SetCustomer(customerInfo);

			// assert
			mockedRepository.Verify(p => p.SetCustomer(It.Is<Customer>(c => c.Id == customerId)));
		}

		[TestMethod]
		public void SetEmployees_RequestNotExistingEmployees_SetsEmployeesInRepository()
		{
			// arrange
			Mock<IRepository> mockedRepository = CreateMockedRepository();
			var service = new CustomerService(mockedRepository.Object);

			const int customerId1 = 23;
			const int customerId2 = 24;

			var customerInfos = new[]
			{
				new CustomerInfo { Id = customerId1 },
				new CustomerInfo { Id = customerId2 },
			};

			// act
			service.SetCustomers(customerInfos);

			// assert
			mockedRepository.Verify(p => p.SetCustomerBatch(It.Is<IEnumerable<Customer>>(c => c.Count() == 2)));
		}

		private Mock<IRepository> CreateMockedRepository()
		{
			var mock = new Mock<IRepository>();

			IEnumerable<Customer> customers = CreateCustomers();
			mock.Setup(p => p.GetCustomers()).Returns(customers);

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
	}
}