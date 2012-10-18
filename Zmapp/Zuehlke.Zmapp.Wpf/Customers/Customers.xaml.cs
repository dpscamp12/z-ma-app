
using System.ComponentModel;

namespace Zuehlke.Zmapp.Wpf.Customers
{
	/// <summary>
	/// Interaction logic for Customers.xaml
	/// </summary>
	public partial class Customers
	{
		private readonly CustomersViewModel viewModel;

		public Customers()
		{
			this.InitializeComponent();

			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				this.viewModel = new CustomersViewModel();
				this.viewModel.Init();
				this.DataContext = this.viewModel;
			}
		}
	}
}
