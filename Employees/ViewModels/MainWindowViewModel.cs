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
    public class MainViewModel : BindableBase
    {
        public ICommand AddNewButtonCommand { get; set; }
        public ICommand EditButtonComand { get; set; }

        private List<EmployeeModel> _employeesList;
        public List<EmployeeModel> EmployeesList { get { return _employeesList; } set { SetProperty(ref _employeesList, value); }
        }
        public EmployeeModel SelectedEmployee { get; set; }

        public MainViewModel()
        {
            LoadData();
        }
   
        public void LoadData()
        {
            using AppDbContext dbContext = new AppDbContext();

            var employees =  dbContext.Employees.Include(x => x.PhoneCode).ToList();

            EmployeesList = employees.ToEmployeeModels().ToList();

            AddNewButtonCommand = new RelayCommand(o => AddNewButtonClick());
            EditButtonComand = new RelayCommand(o => EditEmployeeButtonClick(), IsEmployeeChoosed);
        }

        private void AddNewButtonClick()
        {
            var addViewModel = new AddEditNewViewModel();         
            addViewModel.RefreshDataRequested += LoadData;
            AddEmployeeWindow window = new AddEmployeeWindow(addViewModel);            
            window.Show();
        }

        private void EditEmployeeButtonClick()
        {
            var editViewModel = new AddEditNewViewModel(SelectedEmployee);
            editViewModel.RefreshDataRequested += LoadData;
            AddEmployeeWindow window = new AddEmployeeWindow(editViewModel);
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