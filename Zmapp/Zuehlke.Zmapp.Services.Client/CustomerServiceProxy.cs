using System.Threading.Tasks;
using Zuehlke.Zmapp.Services.Contracts.Customers;

namespace Zuehlke.Zmapp.Services.Client
{
	public class CustomerServiceProxy : ServiceProxy<ICustomerService>, ICustomerServiceAsync
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

		public void SetCustomers(CustomerInfo[] customers)
		{
			this.ExecuteRemoteCall((service) => service.SetCustomers(customers));
		}

		public bool RemoveCustomer(int customerId)
		{
			return this.ExecuteRemoteCall((service) => service.RemoveCustomer(customerId));
		}
		#endregion

        #region Implementation of ICustomerServiceAsync
        public async Task<CustomerInfo[]> GetCustomersAsync()
        {
            return await Task.Factory.StartNew(() =>
                {
                    return this.GetCustomers();
                });
        }
        #endregion
    }
}