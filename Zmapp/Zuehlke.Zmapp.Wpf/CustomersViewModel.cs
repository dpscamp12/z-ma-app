using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf
{
	public class CustomersViewModel : NotificationObject
	{
		private readonly ICustomerService service;
		private readonly List<CustomerInfo> customers = new List<CustomerInfo>();
		private CustomerInfo selectedCustomer;


		public CustomersViewModel()
		{
			this.Init();
		}

		public CustomersViewModel(ICustomerService injectedService)
			: this()
		{
			this.service = injectedService;
		}

		public void Init()
		{
			this.customers.Clear();
			CustomerInfo[] customerInfos = this.service.GetCustomers();
			this.customers.AddRange(customerInfos);

			this.selectedCustomer = this.customers.FirstOrDefault();
		}

		public IEnumerable<CustomerInfo> Customers
		{
			get { return this.customers; }
		}

		public MultiSelectCollectionView<CareerLevel> CareerLevels { get; private set; }


		public CustomerInfo SelectedCustomer
		{
			get
			{
				return this.selectedCustomer;
			}
			set
			{
				this.selectedCustomer = value;
				this.RaisePropertyChanged(() => this.SelectedCustomer);
			}
		}
	}
}
