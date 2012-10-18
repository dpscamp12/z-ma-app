using System.ServiceModel;

namespace Zuehlke.Zmapp.Services.Contracts.Customers
{
	[ServiceContract]
	public interface ICustomerService
	{
		[OperationContract]
		CustomerInfo[] GetCustomers();

		[OperationContract]
		void SetCustomer(CustomerInfo customer);

		[OperationContract]
		void SetCustomers(CustomerInfo[] customers);

		[OperationContract]
		bool RemoveCustomer(int customerId);
	}
}