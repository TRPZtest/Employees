using Employees.Models;
using Employees.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Employees.Views
{
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private AddNewViewModel _viewModel;
        public AddEmployeeWindow()
        {
            var vm = new AddNewViewModel();
            _viewModel = vm;
            DataContext = vm;
            InitializeComponent();
        }

        private void SaveNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_viewModel.Employee.PhoneCode.Code);
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }     
    }
}
