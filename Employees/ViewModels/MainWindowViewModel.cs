using Employees.Commands;
using Employees.Data.Db;
using Employees.Models;
using Employees.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Employees.ViewModels
{
    public class MainViewModel 
    {
        public ICommand AddNewButtonCommand { get; set; }
        public ICommand EditButtonComand { get; set; }

        public MainViewModel()
        {
            LoadDataAsync();
        }

        public ObservableCollection<EmployeeModel> EmployeesList { get; set; }
        public EmployeeModel SelectedEmployee { get; set; }

        public async Task LoadDataAsync()
        {
            using AppDbContext dbContext = new AppDbContext();

            var employees = await dbContext.Employees.Include(x => x.PhoneCode).ToListAsync();

            EmployeesList = new ObservableCollection<EmployeeModel>(employees.ToEmployeeModels());

            AddNewButtonCommand = new RelayCommand(o => AddNewButtonClick());
            EditButtonComand = new RelayCommand(o => EditEmployeeButtonClick(), IsEmployeeChoosed);
        }

        private void AddNewButtonClick()
        {
            AddEmployeeWindow window = new AddEmployeeWindow();
            window.Show();
        }

        private void EditEmployeeButtonClick()
        {
            EditEmployeeWindow window = new EditEmployeeWindow(SelectedEmployee);
            window.Show();
        }

        private bool IsEmployeeChoosed(object o)
        {
            if (SelectedEmployee is null)
                return false;
            else return true;
        }
    }
}