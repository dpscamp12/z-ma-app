using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Client;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf
{
	public class EmployeeListViewModel : NotificationObject
	{
		private readonly ICustomerService customerService;
		private readonly IEmployeeReservationService service;
		private readonly List<CustomerInfo> customers = new List<CustomerInfo>();
		private readonly ObservableCollection<EmployeeSearchResult> availableEmployees = new ObservableCollection<EmployeeSearchResult>();
		private readonly List<Skill> availableSkills = new List<Skill>(typeof(Skill).GetEnumValues().Cast<Skill>());
		private readonly List<CareerLevel> availableCareerLevels = new List<CareerLevel>(typeof(CareerLevel).GetEnumValues().Cast<CareerLevel>());

		private DateTime beginOfWorkPeriod;
		private DateTime endOfWorkPeriod;
		private CustomerInfo selectedCustomer;

		private readonly DelegateCommand<EmployeeSearchResult> reserveEmployeeCommand;

		public EmployeeListViewModel()
			:this(new CustomerServiceProxy(), new EmployeeReservationServiceProxy())
		{
		}

		public EmployeeListViewModel(ICustomerService customerService, IEmployeeReservationService injectedService)
		{
			if (customerService == null) throw new ArgumentNullException("customerService");
			if (injectedService == null) throw new ArgumentNullException("injectedService");

			this.customerService = customerService;
			this.service = injectedService;

			this.Init();
			this.Skills = new MultiSelectCollectionView<Skill>(availableSkills);
			this.CareerLevels = new MultiSelectCollectionView<CareerLevel>(availableCareerLevels);

			this.CareerLevels.SelectedItems.CollectionChanged += this.OnFilterSelectionChanged;
			this.Skills.SelectedItems.CollectionChanged += this.OnFilterSelectionChanged;
			this.PropertyChanged += (o, e) => this.RefreshValues();

			this.reserveEmployeeCommand = new DelegateCommand<EmployeeSearchResult>(this.OnReservationTriggered);
		}

		public void Init()
		{
			this.beginOfWorkPeriod = this.endOfWorkPeriod = DateTime.Now;
			this.customers.Clear();
			CustomerInfo[] customerInfos = this.customerService.GetCustomers();
			this.customers.AddRange(customerInfos);

			this.selectedCustomer = this.customers.FirstOrDefault();
		}

		public IEnumerable<CustomerInfo> Customers
		{
			get { return this.customers; }
		}

		public MultiSelectCollectionView<CareerLevel> CareerLevels { get; private set; }

		public MultiSelectCollectionView<Skill> Skills { get; private set; }

		public DateTime BeginOfWorkPeriod
		{
			get
			{
				return this.beginOfWorkPeriod;
			}
			set
			{
				if (value > this.endOfWorkPeriod)
				{
					throw new ArgumentException("End value must be higher as begin date.");
				}

				this.beginOfWorkPeriod = value;
				this.RaisePropertyChanged(() => this.BeginOfWorkPeriod);
			}
		}

		public DateTime EndOfWorkPeriod
		{
			get
			{
				return this.endOfWorkPeriod;
			}
			set
			{
				if (value < this.beginOfWorkPeriod)
				{
					throw new ArgumentException("End value must be higher as begin date.");
				}

				this.endOfWorkPeriod = value;
				this.RaisePropertyChanged(() => this.EndOfWorkPeriod);
			}
		}

		public ObservableCollection<EmployeeSearchResult> AvailableEmployees
		{
			get { return this.availableEmployees; }
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

		public DelegateCommand<EmployeeSearchResult> ReserveEmployeeCommand
		{
			get { return this.reserveEmployeeCommand; }
		}

		void OnFilterSelectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			this.RefreshValues();
		}

		private void RefreshValues()
		{
			var x = this.CareerLevels.SelectedItems.Count;

			var employeeQuery = new EmployeeQuery()
									{
										CustomerId = this.selectedCustomer.Id,
										BeginOfWorkPeriod = this.BeginOfWorkPeriod,
										EndOfWorkPeriod = this.EndOfWorkPeriod,
										RequestedCareerLevels = this.CareerLevels.SelectedItems.ToArray(),
										RequestedSkills = this.Skills.SelectedItems.ToArray()
									};

			EmployeeSearchResult[] result = this.service.FindPotentialEmployeesForCustomer(employeeQuery);
			var filtered = result.Where(empl => this.CareerLevels.SelectedItems.Contains(empl.Level));
			this.availableEmployees.ReplaceAllItemsWith(filtered);
		}

		private void OnReservationTriggered(EmployeeSearchResult obj)
		{
			service.ReserveEmployeeForCustomer(obj.Id, this.SelectedCustomer.Id, this.BeginOfWorkPeriod, this.EndOfWorkPeriod);
			this.RefreshValues();
		}
	}
}
