using System.ServiceModel;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf
{
    public class EmployeeEvluationServiceProxy : ClientBase<IEmployeeEvaluationService>, IEmployeeEvaluationService
    {
        public CustomerInfo[] GetCustomers()
        {
            return Channel.GetCustomers();
        }

        public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
        {
            return Channel.FindPotentialEmployeesForCustomer(query);
        }
    }
}