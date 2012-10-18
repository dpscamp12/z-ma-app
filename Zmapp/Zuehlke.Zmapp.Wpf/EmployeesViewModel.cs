using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Client;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf
{
	public class EmployeesViewModel : NotificationObject
	{
		private readonly IEmployeeService service = new EmployeeServiceProxy();
		private readonly ObservableCollection<EmployeeInfo> employees = new ObservableCollection<EmployeeInfo>();
		private EmployeeInfo selectedEmployee;

		private readonly DelegateCommand saveCustomer;

		public EmployeesViewModel()
		{
			this.saveCustomer = new DelegateCommand(this.OnSave);
			this.Init();
		}

		public EmployeesViewModel(IEmployeeService injectedService)
			: this()
		{
			this.service = injectedService;
		}

		private void OnSave()
		{
			this.service.SetEmployees(this.employees.ToArray());
		}

		public DelegateCommand SaveEmployeeCommand
		{
			get { return this.saveCustomer; }
		}

		public void Init()
		{
			this.employees.Clear();
			EmployeeInfo[] employeeInfos = this.service.GetEmployees();
			this.employees.ReplaceAllItemsWith(employeeInfos);

			this.selectedEmployee = this.employees.FirstOrDefault();
		}

		public ObservableCollection<EmployeeInfo> Employees
		{
			get { return this.employees; }
		}

		public EmployeeInfo SelectedEmployee
		{
			get
			{
				return this.selectedEmployee;
			}
			set
			{
				this.selectedEmployee = value;
				this.RaisePropertyChanged(() => this.SelectedEmployee);
			}
		}
	}
}
