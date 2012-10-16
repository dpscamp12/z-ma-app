using System;
using System.ServiceModel;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf
{
    public class EmployeeEvaluationServiceProxy : IEmployeeEvaluationService
    {
        public CustomerInfo[] GetCustomers()
        {
            return this.ExecuteRemoteCall<IEmployeeEvaluationService, CustomerInfo[]>(
                (service) => service.GetCustomers());
        }

        public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
        {
            return this.ExecuteRemoteCall<IEmployeeEvaluationService, EmployeeSearchResult[]>(
                (service) => service.FindPotentialEmployeesForCustomer(query));
        }

        private TResult ExecuteRemoteCall<TService, TResult>(Func<TService, TResult> function)
        {
            var fab = new ChannelFactory<TService>("BasicHttpBinding_IEmployeeEvaluationService");
            fab.Open();
            var channel = fab.CreateChannel();

            TResult result;
            try
            {
                ((ICommunicationObject)channel).Open();
                result = function(channel);
            }
            finally
            {
                ((ICommunicationObject)channel).Close();
                fab.Close();
            }

            return result;
        }
    }
}