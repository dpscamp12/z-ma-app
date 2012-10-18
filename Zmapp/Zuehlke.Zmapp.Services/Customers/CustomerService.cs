using System;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Data;
using Zuehlke.Zmapp.Services.DomainModel;

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
				.ToArray();
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

		public void SetCustomers(CustomerInfo[] customers)
		{
			var customersEntities = customers.Select((ci) =>
			new Customer
			{
				Id = ci.Id,
				Name = ci.Name,
				Street = ci.Street,
				City = ci.City,
				ZipCode = ci.ZipCode
			});

			this.repository.SetCustomerBatch(customersEntities);
		}

		public bool RemoveCustomer(int customerId)
		{
			return this.repository.RemoveCustomer(customerId);
		}

		#endregion
	}
}