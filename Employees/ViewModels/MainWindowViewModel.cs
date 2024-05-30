using Employees.Commands;
using Employees.Data.Db;
using Employees.Models;
using Employees.Services;
using Employees.Utils;
using Employees.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Employees.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string _fullNameSearchString;
        public string FullNameSearchString { get { return _fullNameSearchString; } set { SetProperty(ref _fullNameSearchString, value); } }
        public ICommand AddNewButtonCommand { get; set; }
        public ICommand EditButtonCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ResetSearchCommand { get; set; }

        private List<EmployeeModel> _employeesList;
        public List<EmployeeModel> EmployeesList { get { return _employeesList; } set { SetProperty(ref _employeesList, value); } }
        public EmployeeModel SelectedEmployee { get; set; }

        public MainViewModel()
        {
            LoadData();

            AddNewButtonCommand = new RelayCommand(o => AddNewButtonClick());
            EditButtonCommand = new RelayCommand(o => EditEmployeeButtonClick(), IsEmployeeChoosed);
            ExportCommand = new RelayCommand(async o => await ExportButtonClick());
            ImportCommand = new RelayCommand(async (o) => await ImportButtonClick());
            DeleteCommand = new RelayCommand(async o => await DeleteButtonClick(), IsEmployeeChoosed);
            SearchCommand = new RelayCommand(o => SearchClick());
            ResetSearchCommand = new RelayCommand(o =>  ResetSearchClick());
        }
   
        public void LoadData()
        {          
            using var db = new EmployeeService();
                
            EmployeesList = db.GetAsync(FullNameSearchString).Result.ToEmployeeModels().ToList();     
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

        private async Task DeleteButtonClick()
        {
            try
            {
                using var db = new EmployeeService();

                await db.DeleteById(SelectedEmployee.Id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting data: {ex.Message}");
            }
        }

        private async Task ImportButtonClick()
        {
            var dialog = new OpenFileDialog();  
            dialog.Filter = "Json files (*.json)|*.json";
            dialog.CheckFileExists = true;
            dialog.ShowDialog();


            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                var employeeModels =  await FileUtils.DeserializeFromFile<List<EmployeeModel>>(dialog.FileName);

                try
                {
                    using var db = new EmployeeService();

                    var entites = employeeModels?.ToEmployeeEntities();

                    await db.AddAsync(entites);
                    
                    LoadData();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error while inmporting data: {ex.Message}");
                }
            }
        }

        private async Task ExportButtonClick()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Json files (*.json)|*.json";

            dialog.ShowDialog();

            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                await FileUtils.SerializeToFile(_employeesList, dialog.FileName);
            }
        }

        private bool IsEmployeeChoosed(object o)
        {
            if (SelectedEmployee is null)
                return false;
            else return true;
        }

        public void SearchClick()
        {
            LoadData();
        }

        public void ResetSearchClick()
        {
            FullNameSearchString = string.Empty;
            LoadData();
        }
    }
}