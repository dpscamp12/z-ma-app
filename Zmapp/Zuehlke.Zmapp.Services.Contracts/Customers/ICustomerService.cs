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
		bool RemoveCustomer(int customerId);
	}
}