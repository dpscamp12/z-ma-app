using System;
using System.ServiceModel;

namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
    [ServiceContract]
    public interface IEmployeeEvaluationService
    {
        [OperationContract]
        CustomerInfo[] GetCustomers();

        [OperationContract]
        EmployeeSearchResult[] FindPotentialEmployeesForCustomer(int customerId, Skill[] skills, CareerLevel[] level, DateTime from, DateTime though);
    }
}
