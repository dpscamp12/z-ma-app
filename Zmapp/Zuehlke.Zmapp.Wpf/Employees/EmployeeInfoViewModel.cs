using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.Wpf.Employees
{
	public class EmployeeInfoViewModel : NotificationObject
	{
		private readonly EmployeeInfo employeeInfoInstance;

		public EmployeeInfoViewModel(EmployeeInfo info)
		{
			this.employeeInfoInstance = info;
			this.Skills = new ObservableCollection<Skill>(info.Skills);
		}

		public EmployeeInfo EmployeeInfoInstance
		{
			get
			{
				this.employeeInfoInstance.Skills = this.Skills.ToArray();
				return this.employeeInfoInstance;
			}
		}

		public string LastName
		{
			get
			{
				return this.employeeInfoInstance.LastName;
			}
			set
			{
				if (this.employeeInfoInstance.LastName != value)
				{
					this.employeeInfoInstance.LastName = value;
					this.RaisePropertyChanged(() => this.LastName);
				}
			}
		}

		public string FirstName
		{
			get
			{
				return this.employeeInfoInstance.FirstName;
			}
			set
			{
				this.employeeInfoInstance.FirstName = value;
				this.RaisePropertyChanged(() => this.FirstName);
			}
		}

		public int Id
		{
			get
			{
				return this.employeeInfoInstance.Id;
			}
			set
			{
				this.employeeInfoInstance.Id = value;
				this.RaisePropertyChanged(() => this.Id);
			}
		}

		public string Phone
		{
			get
			{
				return this.employeeInfoInstance.Phone;
			}
			set
			{
				this.employeeInfoInstance.Phone = value;
				this.RaisePropertyChanged(() => this.Phone);
			}
		}

		public string Email
		{
			get
			{
				return this.employeeInfoInstance.EMail;
			}
			set
			{
				this.employeeInfoInstance.EMail = value;
				this.RaisePropertyChanged(() => this.Email);
			}
		}

		public string Street
		{
			get
			{
				return this.employeeInfoInstance.Street;
			}
			set
			{
				this.employeeInfoInstance.FirstName = value;
				this.RaisePropertyChanged(() => this.FirstName);
			}
		}

		public int ZipCode
		{
			get
			{
				return this.employeeInfoInstance.ZipCode;
			}
			set
			{
				this.employeeInfoInstance.ZipCode = value;
				this.RaisePropertyChanged(() => this.ZipCode);
			}
		}

		public string City
		{
			get
			{
				return this.employeeInfoInstance.City;
			}
			set
			{
				this.employeeInfoInstance.City = value;
				this.RaisePropertyChanged(() => this.City);
			}
		}

		public CareerLevel Level
		{
			get
			{
				return this.employeeInfoInstance.CareerLevel;
			}
			set
			{
				this.employeeInfoInstance.CareerLevel = value;
				this.RaisePropertyChanged(() => this.Level);
			}
		}

		public ObservableCollection<Skill> Skills { get; private set; }
	}
}