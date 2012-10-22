using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zuehlke.Zmapp.Services.Contracts.Customers;

namespace Zuehlke.Zmapp.Services.Client
{
    public interface ICustomerServiceAsync : ICustomerService
    {
        Task<CustomerInfo[]> GetCustomersAsync();
    }
}
