using System.ServiceModel;
using Zuehlke.Zmapp.Services.Contracts.Employee;

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
		void RemoveCustomer(int customerId);
	}
}