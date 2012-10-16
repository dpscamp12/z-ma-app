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

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			this.ExecuteRemoteCall<IEmployeeEvaluationService>(
				(service) => service.ReserveEmployeeForCustomer(employeeId, customerId, beginOfPeriod, endOfPeriod));
		}

		private void ExecuteRemoteCall<TService>(Action<TService> action)
		{
			var fab = new ChannelFactory<TService>("BasicHttpBinding_IEmployeeEvaluationService");
			fab.Open();
			var channel = fab.CreateChannel();

			try
			{
				((ICommunicationObject)channel).Open();
				action(channel);
			}
			finally
			{
				((ICommunicationObject)channel).Close();
				fab.Close();
			}
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