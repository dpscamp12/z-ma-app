﻿using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Client;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf
{
	public class CustomersViewModel : NotificationObject
	{
		private readonly ICustomerService service = new CustomerServiceProxy();
		private readonly ObservableCollection<CustomerInfo> customers = new ObservableCollection<CustomerInfo>();
		private CustomerInfo selectedCustomer;

		private readonly PrismReplacementDelegateCommand<CustomerInfo> saveCustomer;

		public CustomersViewModel()
		{
			this.saveCustomer = new PrismReplacementDelegateCommand<CustomerInfo>(this.OnSave);

			this.Init();
		}

		public CustomersViewModel(ICustomerService injectedService)
			: this()
		{
			this.service = injectedService;
		}

		private void OnSave(CustomerInfo obj)
		{
			if (obj == null) return;
			this.service.SetCustomer(obj);
		}

		public PrismReplacementDelegateCommand<CustomerInfo> SaveCustomerCommand
		{
			get { return this.saveCustomer; }
		}

		public void Init()
		{
			this.customers.Clear();
			CustomerInfo[] customerInfos = this.service.GetCustomers();
			this.customers.ReplaceAllItemsWith(customerInfos);

			this.selectedCustomer = this.customers.FirstOrDefault();
		}

		public ObservableCollection<CustomerInfo> Customers
		{
			get { return this.customers; }
		}

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
