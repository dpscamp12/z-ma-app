using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf
{
    public class EmployeeListViewModel : NotificationObject
    {
        private readonly IEmployeeEvaluationService service = new EmployeeEvaluationServiceProxy();
        private readonly List<CustomerInfo> customers = new List<CustomerInfo>();
        private CustomerInfo selectedCustomer;
        private readonly ObservableCollection<EmployeeSearchResult> availableEmployees = new ObservableCollection<EmployeeSearchResult>();
        private readonly List<Skill> availableSkills = new List<Skill>(typeof(Skill).GetEnumValues().Cast<Skill>());
        private readonly List<CareerLevel> availableCareerLevels = new List<CareerLevel>(typeof(CareerLevel).GetEnumValues().Cast<CareerLevel>());

        public EmployeeListViewModel()
        {
            this.Init();
            this.Skills = new MultiSelectCollectionView<Skill>(availableSkills);
            this.CareerLevels = new MultiSelectCollectionView<CareerLevel>(availableCareerLevels);

            this.CareerLevels.SelectedItems.CollectionChanged += this.OnFilterSelectionChanged;
            this.Skills.SelectedItems.CollectionChanged += this.OnFilterSelectionChanged;
        }

        public EmployeeListViewModel(IEmployeeEvaluationService injectedService)
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

        public MultiSelectCollectionView<Skill> Skills { get; private set; }

        public DateTime BeginOfWorkPeriod { get; set; }

        public DateTime EndOfWorkPeriod { get; set; }

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

        void OnFilterSelectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var x = CareerLevels.SelectedItems.Count;

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
    }
}
