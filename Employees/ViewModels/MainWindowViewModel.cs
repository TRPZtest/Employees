using Employees.Commands;
using Employees.Data.Db;
using Employees.Models;
using Employees.Utils;
using Employees.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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
        public ICommand EditButtonCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private List<EmployeeModel> _employeesList;
        public List<EmployeeModel> EmployeesList { get { return _employeesList; } set { SetProperty(ref _employeesList, value); }
        }
        public EmployeeModel SelectedEmployee { get; set; }

        public MainViewModel()
        {
            LoadData();

            AddNewButtonCommand = new RelayCommand(o => AddNewButtonClick());
            EditButtonCommand = new RelayCommand(o => EditEmployeeButtonClick(), IsEmployeeChoosed);
            ExportCommand = new RelayCommand(async o => await ExportButtonClick());
            ImportCommand = new RelayCommand(async (o) => await ImportButtonClick());
            DeleteCommand = new RelayCommand(o => DeleteButtonClick(), IsEmployeeChoosed);
        }
   
        public void LoadData()
        {
            using AppDbContext dbContext = new AppDbContext();

            var employees =  dbContext.Employees
                .Include(x => x.PhoneCode)
                .AsNoTracking()
                .ToList();

            EmployeesList = employees.ToEmployeeModels().ToList();       
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

        private void DeleteButtonClick()
        {
            try
            {
                using var db = new AppDbContext();

                db.Employees.Where(x => x.Id == SelectedEmployee.Id).ExecuteDelete();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting data: {ex.Message}");
            }
        }

        private async Task ImportButtonClick()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Json files (*.json)|*.json";

            dialog.ShowDialog();


            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                var employeeModels =  await FileUtils.DeserializeFromFile<List<EmployeeModel>>(dialog.FileName);

                try
                {
                    using var db = new AppDbContext();

                    var entites = employeeModels?.ToEmployeeEntities();

                    await db.AddRangeAsync(entites);
                    await db.SaveChangesAsync();

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
    }
}