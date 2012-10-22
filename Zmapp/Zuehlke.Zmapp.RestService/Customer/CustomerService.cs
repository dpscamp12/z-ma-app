using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Data;

namespace Zuehlke.Zmapp.RestService.Customer
{
    [Route("/Customers")]
    public class GetCustomersRequest : IReturn<CustomerInfo[]>
    {
    }

    [Route("/Customers/{Id}")]
    public class SearchCustomerByIdRequest : IReturn<CustomerInfo>
    {
        public int Id { get; set; }
    }

    [Route("/Customers/search/{Name}")]
    public class SearchCustomersByNameRequest : IReturn<CustomerInfo[]>
    {
        public string Name { get; set; }
    }
     
    public class CustomerService : IService
    {
        public IRepository Repository { get; set; }

        public object Get(GetCustomersRequest request)
        {
            return this.Repository.GetCustomers();
        }

        public object Get(SearchCustomerByIdRequest request)
        {
            return this.Repository.GetCustomer(request.Id);
        }

        public object Get(SearchCustomersByNameRequest request)
        {
            return this.Repository.GetCustomers().Where(p => p.Name.Contains(request.Name));
        }
    }
}
