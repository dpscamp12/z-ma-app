
using System.ComponentModel;

namespace Zuehlke.Zmapp.Wpf
{
	/// <summary>
	/// Interaction logic for EmployeeReservation.xaml
	/// </summary>
	public partial class EmployeeReservation
	{
		private readonly EmployeeReservationViewModel viewModel;

		public EmployeeReservation()
		{
			this.InitializeComponent();

			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				this.viewModel = new EmployeeReservationViewModel();
				this.viewModel.Init();
				this.DataContext = this.viewModel;
			}
		}
	}
}
