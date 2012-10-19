using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Wpf.Tools;

namespace Zuehlke.Zmapp.Wpf.Employees
{
	public class EmployeeInfoViewModel : NotificationObject
	{
		private readonly EmployeeInfo employeeInfoInstance;
		private readonly List<Skill> availableSkills = new List<Skill>(typeof(Skill).GetEnumValues().Cast<Skill>());

		public EmployeeInfoViewModel(EmployeeInfo info)
		{
			this.employeeInfoInstance = info;
			this.Skills = new MultiSelectCollectionView<Skill>(this.availableSkills);
			foreach (var skill in info.Skills)
			{
				this.Skills.SelectedItems.Add(skill);
			}
		}

		public EmployeeInfo EmployeeInfoInstance
		{
			get
			{
				this.employeeInfoInstance.Skills = this.Skills.SelectedItems.ToArray();
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

		public MultiSelectCollectionView<Skill> Skills { get; private set; }
	}
}