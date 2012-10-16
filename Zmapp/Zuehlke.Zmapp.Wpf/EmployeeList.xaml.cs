using System.Windows;

namespace Zuehlke.Zmapp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeeListViewModel viewModel = new EmployeeListViewModel();

        public MainWindow()
        {
            this.InitializeComponent();
            //this.viewModel.Init();
            this.DataContext = this.viewModel;
        }
    }
}
