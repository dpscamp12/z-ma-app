using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf
{
    public class EmployeeListViewModel : NotificationObject
    {
        private readonly IEmployeeEvaluationService service = new EmployeeEvaluationServiceMock();
        private readonly List<CustomerInfo> customers = new List<CustomerInfo>();
        private CustomerInfo selectedCustomer;
        private readonly ObservableCollection<EmployeeSearchResult> availableEmployees = new ObservableCollection<EmployeeSearchResult>();

        public EmployeeListViewModel()
        {
        }

        public EmployeeListViewModel(IEmployeeEvaluationService injectedService)
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

        public IEnumerable<Skill> Skills
        {
            get { return typeof(Skill).GetEnumValues().Cast<Skill>(); }
        }

        public IEnumerable<Skill> SelectedSkills { get; set; }

        public IEnumerable<CareerLevel> SelectedLevels { get; set; }

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
    }
}
