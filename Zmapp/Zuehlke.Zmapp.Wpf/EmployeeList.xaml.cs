using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Console.WriteLine(typeof(IEmployeeEvaluationService).AssemblyQualifiedName);
            //var x = new Test();
            //x.Open();
            //var y = x.GetCustomers();
            //x.Close();
        }
    }

    public class Test : ClientBase<IEmployeeEvaluationService>, IEmployeeEvaluationService
    {
        public CustomerInfo[] GetCustomers()
        {
            return Channel.GetCustomers();
        }

        public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
        {
            return Channel.FindPotentialEmployeesForCustomer(query);
        }
    }
}
