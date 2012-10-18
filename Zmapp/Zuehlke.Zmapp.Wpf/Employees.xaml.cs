using System.Windows;

namespace Zuehlke.Zmapp.Wpf
{
	/// <summary>
	/// Interaction logic for Employees.xaml
	/// </summary>
	public partial class Employees : Window
	{
		private readonly EmployeesViewModel viewModel = new EmployeesViewModel();

		public Employees()
		{
			InitializeComponent();

			this.viewModel.Init();
			this.DataContext = this.viewModel;
		}
	}
}
