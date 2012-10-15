namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
    public class EmployeeEvaluationServiceMock : IEmployeeEvaluationService
    {
        public CustomerInfo[] GetCustomers()
        {
            return new[]
                       {
                           new CustomerInfo() { Id = 1, Name = "Customer, First" }, 
                           new CustomerInfo() { Id = 2, Name = "Customer, Second" },
                           new CustomerInfo() { Id = 3, Name = "Customer, Third" }
                       };
        }

        public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
        {
            var e = new EmployeeSearchResult()
                        {
                            Distance = 10.3f,
                            EmployeeName = "Hans Wurst",
                            Id = 1,
                            Level = CareerLevel.SoftwareEngineer,
                            Skills = new[] { Skill.WCF, Skill.AspDotNet, }
                        };

            return new[] { e };
        }
    }
}