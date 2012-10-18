using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zuehlke.Zmapp.Services.Contracts.Customers;

using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.Wpf.Tests
{
	[TestClass]
	public class EmployeeListViewModelTests
	{
		[TestMethod]
		public void Skills_AllEnumValuesReturned()
		{
			var customerServiceMock = GetMockedCustomerService();
			var viewModel = new EmployeeListViewModel(customerServiceMock.Object, new EmployeeReservationServiceMock());
			Assert.AreEqual(7, viewModel.Skills.Count);
			Assert.IsTrue(viewModel.Skills.Contains(Skill.WCF));
		}

		[TestMethod]
		public void Init_FillsAvailableCustomers()
		{
			// Arrange
			var customerServiceMock = GetMockedCustomerService();
			var mock = GetMockedService();
			var viewModel = new EmployeeListViewModel(customerServiceMock.Object, mock.Object);

			// Act
			viewModel.Init();

			// Assert 
			Assert.IsTrue(viewModel.Customers.Any(c => c.Id == 1));
		}

		[TestMethod]
		public void Init_FirstCustomerSelected_SelectedCustomerValueWasSet()
		{
			// Arrange
			var mock = GetMockedService();
			var customerServiceMock = GetMockedCustomerService();
			var viewModel = new EmployeeListViewModel(customerServiceMock.Object, mock.Object);

			// Act
			viewModel.Init();

			// Assert
			Assert.AreEqual(1, viewModel.SelectedCustomer.Id);
		}

		private static Mock<IEmployeeReservationService> GetMockedService()
		{
			var mockedService = new Mock<IEmployeeReservationService>();

			return mockedService;
		}

		private static Mock<ICustomerService> GetMockedCustomerService()
		{
			var mockedService = new Mock<ICustomerService>();
			var customerInfos = new[]
                                    {
                                        new CustomerInfo() {Id = 1, Name = "TestCustomer"},
                                        new CustomerInfo() {Id = 2, Name = "Second TestCustomer"},
                                        new CustomerInfo() {Id = 3, Name = "Third TestCustomer"}
                                    };
			mockedService.Setup(mock => mock.GetCustomers()).Returns(customerInfos);

			return mockedService;
		}
	}
}
