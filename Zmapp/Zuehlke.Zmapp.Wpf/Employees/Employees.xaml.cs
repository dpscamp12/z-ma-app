
using System.ComponentModel;

namespace Zuehlke.Zmapp.Wpf.Employees
{
	/// <summary>
	/// Interaction logic for Employees.xaml
	/// </summary>
	public partial class Employees
	{
		private readonly EmployeesViewModel viewModel;

		public Employees()
		{
			this.InitializeComponent();

			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				this.viewModel = new EmployeesViewModel();
				this.viewModel.Init();
				this.DataContext = this.viewModel;
			}
		}
	}
}
