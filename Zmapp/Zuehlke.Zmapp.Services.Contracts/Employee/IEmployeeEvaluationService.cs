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
}
