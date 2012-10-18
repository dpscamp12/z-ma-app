
namespace Zuehlke.Zmapp.Wpf
{
	/// <summary>
	/// Interaction logic for EmployeeReservation.xaml
	/// </summary>
	public partial class EmployeeReservation
	{
		private readonly EmployeeListViewModel viewModel = new EmployeeListViewModel();

		public EmployeeReservation()
		{
			this.InitializeComponent();

			this.viewModel.Init();
			this.DataContext = this.viewModel;
		}
	}
}
