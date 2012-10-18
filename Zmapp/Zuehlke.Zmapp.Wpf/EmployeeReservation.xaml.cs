
namespace Zuehlke.Zmapp.Wpf
{
	/// <summary>
	/// Interaction logic for EmployeeReservation.xaml
	/// </summary>
	public partial class EmployeeReservation
	{
		private readonly EmployeeReservationViewModel viewModel = new EmployeeReservationViewModel();

		public EmployeeReservation()
		{
			this.InitializeComponent();

			this.viewModel.Init();
			this.DataContext = this.viewModel;
		}
	}
}
