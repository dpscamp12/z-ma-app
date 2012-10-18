using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Client;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf.Employees
{
	public class EmployeesViewModel : NotificationObject
	{
		private readonly IEmployeeService service = new EmployeeServiceProxy();
		private readonly ObservableCollection<EmployeeInfoViewModel> employees = new ObservableCollection<EmployeeInfoViewModel>();
		private EmployeeInfoViewModel selectedEmployee;
		private readonly List<Skill> availableSkills = new List<Skill>(typeof(Skill).GetEnumValues().Cast<Skill>());

		private readonly DelegateCommand saveEmployees;

		public EmployeesViewModel()
		{
			this.saveEmployees = new DelegateCommand(this.OnSave);
			this.Init();
		}

		public EmployeesViewModel(IEmployeeService injectedService)
			: this()
		{
			this.service = injectedService;
		}

		private void OnSave()
		{
			this.service.SetEmployees(this.employees.Select((eivm) => eivm.EmployeeInfoInstance).ToArray());
		}

		public DelegateCommand SaveEmployeeCommand
		{
			get { return this.saveEmployees; }
		}

		public void Init()
		{
			this.Skills = new MultiSelectCollectionView<Skill>(this.availableSkills);

			this.employees.Clear();
			var employeeInfos = this.service.GetEmployees().Select((empl) => new EmployeeInfoViewModel(empl));
			this.employees.ReplaceAllItemsWith(employeeInfos);

			this.selectedEmployee = this.employees.FirstOrDefault();
		}

		public ObservableCollection<EmployeeInfoViewModel> Employees
		{
			get { return this.employees; }
		}

		public MultiSelectCollectionView<Skill> Skills { get; private set; }

		public EmployeeInfoViewModel SelectedEmployee
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
