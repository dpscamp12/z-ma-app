using Zuehlke.Zmapp.Services.Contracts.Customers;

namespace Zuehlke.Zmapp.Services.Client
{
	public class CustomerServiceProxy : ServiceProxy<ICustomerService>, ICustomerService
	{
		public CustomerServiceProxy()
			: base("CustomerService")
		{
		}

		#region Implementation of ICustomerService
		public CustomerInfo[] GetCustomers()
		{
			return this.ExecuteRemoteCall((service) => service.GetCustomers());
		}

		public void SetCustomer(CustomerInfo customer)
		{
			this.ExecuteRemoteCall((service) => service.SetCustomer(customer));
		}

		public bool RemoveCustomer(int customerId)
		{
			return this.ExecuteRemoteCall((service) => service.RemoveCustomer(customerId));
		}
		#endregion
	}
}