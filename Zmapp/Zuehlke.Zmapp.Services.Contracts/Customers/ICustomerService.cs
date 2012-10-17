using System.ServiceModel;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Contracts.Customers
{
	[ServiceContract]
	public interface ICustomerService
	{
		[OperationContract]
		CustomerInfo[] GetCustomers();
	}
}