using Employees.ViewModels;
using Employees.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeViewModel _viewModel;

        public MainWindow()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            
            InitializeComponent();              
            
            _viewModel = vm;
            DataContext = vm;
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow window = new AddEmployeeWindow();           
            window.Show();          
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {          
        }
    }
}