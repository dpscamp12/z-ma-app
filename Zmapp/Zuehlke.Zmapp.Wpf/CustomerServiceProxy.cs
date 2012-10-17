using Zuehlke.Zmapp.Services.Contracts.Customers;

namespace Zuehlke.Zmapp.Wpf
{
	public class CustomerServiceProxy : ServiceProxy, ICustomerService
	{
		public CustomerServiceProxy()
			: base("BasicHttpBinding_ICustomerService")
		{
		}

		#region Implementation of ICustomerService
		public CustomerInfo[] GetCustomers()
		{
			return this.ExecuteRemoteCall<ICustomerService, CustomerInfo[]>(
				(service) => service.GetCustomers());
		}

		public void SetCustomer(CustomerInfo customer)
		{
			this.ExecuteRemoteCall<ICustomerService>(
				(service) => service.SetCustomer(customer));
		}

		public void RemoveCustomer(int customerId)
		{
			this.ExecuteRemoteCall<ICustomerService>(
				(service) => service.RemoveCustomer(customerId));
		}
		#endregion
	}
}