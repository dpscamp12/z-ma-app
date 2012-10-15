using System.ServiceModel;

namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
    [ServiceContract]
    public interface IEmployeeEvaluationService
    {
        [OperationContract]
        CustomerInfo[] GetCustomers();

        [OperationContract]
        EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query);
    }

    public class EmployeeEvaluationServiceMock : IEmployeeEvaluationService
    {
        public CustomerInfo[] GetCustomers()
        {
            return new[] { new CustomerInfo() { Id = 1, Name = "Customer 1" } };
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
