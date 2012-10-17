using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf
{
	public class PrismReplacementDelegateCommand<T> : DelegateCommandBase where T : class
	{
		public PrismReplacementDelegateCommand(Action<T> executeMethod)
			: this(executeMethod, (o) => true)
		{
		}

		public PrismReplacementDelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
			: base((o) => executeMethod(GetObjectSafeCastedAs<T>(o)), (o) => canExecuteMethod(GetObjectSafeCastedAs<T>(o)))
		{
			if (executeMethod == null || canExecuteMethod == null)
				throw new ArgumentNullException("executeMethod", "Delegates cannot be null");

#if !WINDOWS_PHONE
			Type genericType = typeof(T);

			if (genericType.IsValueType)
			{
				if ((!genericType.IsGenericType) || (!typeof(Nullable<>).IsAssignableFrom(genericType.GetGenericTypeDefinition())))
				{
					throw new InvalidCastException("DelegateCommand Invalid Generic Payload Type");
				}
			}
#endif
		}

		public bool CanExecute(T parameter)
		{
			return base.CanExecute(parameter);
		}

		public void Execute(T parameter)
		{
			base.Execute(parameter);
		}

		private static T GetObjectSafeCastedAs<T>(object objToCast)
		{
			if (objToCast == null) return default(T);

			if (objToCast.GetType().IsAssignableFrom(typeof(T)))
			{
				return (T)objToCast;
			}
			else return default(T);
		}
	}

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
