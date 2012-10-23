using System.Collections.Generic;
using System.Linq;

using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

using Zuehlke.Zmapp.RestService.Services;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.DomainModel;

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

		public void SetCustomers(CustomerInfo[] customers)
		{
			this.ExecuteRemoteCall((service) => service.SetCustomers(customers));
		}

		public bool RemoveCustomer(int customerId)
		{
			return this.ExecuteRemoteCall((service) => service.RemoveCustomer(customerId));
		}
		#endregion
	}

    public class CustomerServiceProxyRest : ICustomerService
    {
        private const string ServiceUrl = "http://localhost:1337";

        #region Implementation of ICustomerService

        public CustomerInfo[] GetCustomers()
        {
            var client = (IRestClient)new JsonServiceClient(ServiceUrl);
            var result = client.Get(new GetCustomersRequest());

            return result.Select(
                                 s => new CustomerInfo
                                     {
                                         Id = s.Id,
                                         City = s.City,
                                         Name = s.Name,
                                         Street = s.Street,
                                         ZipCode = s.ZipCode
                                     }).ToArray();
        }

        public void SetCustomer(CustomerInfo customer)
        {
            var customerConverted = new Customer
                {
                    Id = customer.Id,
                    City = customer.City,
                    Name = customer.Name,
                    Street = customer.Street,
                    ZipCode = customer.ZipCode
                };

            var client = (IRestClient)new JsonServiceClient();
            client.Post(
                        new SaveCustomersRequest
                            {
                                Customers = new List<Customer>
                                    {
                                        customerConverted
                                    }
                            });
        }

        public void SetCustomers(CustomerInfo[] customers)
        {
            var customersConverted = customers.Select(
                                                      s => new Customer
                                                          {
                                                              Id = s.Id,
                                                              City = s.City,
                                                              Name = s.Name,
                                                              Street = s.Street,
                                                              ZipCode = s.ZipCode
                                                          }).ToList();

            var client = (IRestClient)new JsonServiceClient(ServiceUrl);
            client.Post(
                        new SaveCustomersRequest
                            {
                                Customers = customersConverted
                            });
        }

        public bool RemoveCustomer(int customerId)
        {
            var client = (IRestClient)new JsonServiceClient();
            client.Delete(
                          new DeleteCustomerByIdRequest
                              {
                                  Id = customerId
                              });

            return true;
        }

        #endregion
    }
}