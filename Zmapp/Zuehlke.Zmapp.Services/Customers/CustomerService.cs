using System;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Customers
{
	public class CustomerService : ICustomerService
	{
		private readonly IRepository repository;

		public CustomerService()
			: this(Repository.Instance)
		{
		}

		internal CustomerService(IRepository repository)
		{
			if (repository == null) throw new ArgumentNullException("repository");

			this.repository = repository;
		}

		#region ICustomerService members
		public CustomerInfo[] GetCustomers()
		{
			return this.repository.GetCustomers()
				.Select(c => new CustomerInfo { Id = c.Id, Name = c.Name, Street = c.Street, City = c.City, ZipCode = c.ZipCode })
				.ToArray(); ;
		}

		public void SetCustomer(CustomerInfo customer)
		{
			var customerEnity = new Customer
									{
										Id = customer.Id,
										Name = customer.Name,
										Street = customer.Street,
										City = customer.City,
										ZipCode = customer.ZipCode
									};

			this.repository.SetCustomer(customerEnity);
		}

		public void RemoveCustomer(int customerId)
		{
			this.repository.RemoveCustomer(customerId);
		}

		#endregion
	}
}