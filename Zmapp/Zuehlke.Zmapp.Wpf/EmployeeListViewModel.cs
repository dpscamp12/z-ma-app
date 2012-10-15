using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf
{
    public class EmployeeListViewModel
    {
        private readonly IEmployeeEvaluationService service = new EmployeeEvaluationServiceMock();

        public EmployeeListViewModel()
        {
        }

        public EmployeeListViewModel(IEmployeeEvaluationService injectedService)
        {
            this.service = injectedService;
        }

        public IEnumerable<CustomerInfo> Customers
        {
            get
            {
                return service.GetCustomers();
            }
        }

        public IEnumerable<Skill> Skills
        {
            get { return typeof(Skill).GetEnumValues().Cast<Skill>(); }
        }
    }
}
