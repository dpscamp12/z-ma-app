using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf.Tests
{
    [TestClass]
    public class EmployeeListViewModelTests
    {
        [TestMethod]
        public void Skills_AllEnumValuesReturned()
        {
            var viewModel = new EmployeeListViewModel(new EmployeeEvaluationServiceMock());
            Assert.AreEqual(6, viewModel.Skills.Count());
            Assert.IsTrue(viewModel.Skills.Contains(Skill.WCF));
        }
    }
}
