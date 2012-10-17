using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.Wpf.Converters;

namespace Zuehlke.Zmapp.Wpf.Tests
{
    [TestClass]
    public class CustomerToCityStringConverterTests
    {
        [TestMethod]
        public void Convert_CombinesZipCodeAndCity()
        {
            // Arrange
            var converter = new CustomerToCityStringConverter();
            var customerInfo = new CustomerInfo() { City = "Zürich", Id = 1, Name = "Customer, First", Street = "Bahnhofstrasse 1", ZipCode = "8000" };

            // Act
            string combined = converter.Convert(customerInfo, null, null, CultureInfo.CurrentCulture).ToString();

            // Assert
            Assert.AreEqual("8000 Zürich", combined);
        }
    }
}