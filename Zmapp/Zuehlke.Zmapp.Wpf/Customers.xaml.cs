
namespace Zuehlke.Zmapp.Wpf
{
	/// <summary>
	/// Interaction logic for Customers.xaml
	/// </summary>
	public partial class Customers
	{
		private readonly CustomersViewModel viewModel = new CustomersViewModel();

		public Customers()
		{
			InitializeComponent();

			this.viewModel.Init();
			this.DataContext = this.viewModel;
		}
	}
}
