﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf
{
    public class EmployeeListViewModel : NotificationObject
    {
        private readonly IEmployeeEvaluationService service = new EmployeeEvaluationServiceMock();
        private readonly List<CustomerInfo> customers = new List<CustomerInfo>();
        private CustomerInfo selectedCustomer;

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
