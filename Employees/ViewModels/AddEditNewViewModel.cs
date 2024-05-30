using Employees.Commands;
using Employees.Data.Db;
using Employees.Data.Db.Entities;
using Employees.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Employees.ViewModels
{
    public class AddEditNewViewModel
    {
        public ICommand SaveButtonCommand { get; set; }
        public event Action RefreshDataRequested;
        public List<PhoneCode> PhoneCodes { get; set; }
        public EmployeeModel Employee { get; set; }

        public AddEditNewViewModel() 
        {
            Employee = new EmployeeModel();
            SaveButtonCommand = new RelayCommand(async o => await SaveNew(o), o => IsValid());
            LoadDataAsync();       
        }   

        public AddEditNewViewModel(EmployeeModel existedEmployee) 
        {        
            Employee = existedEmployee;

            SaveButtonCommand = new RelayCommand(async o => await EditExisted(o), o => IsValid());

            LoadDataAsync();
        }
            
        private async Task LoadDataAsync()
        {
            using var db = new AppDbContext();
            PhoneCodes = await db.PhoneCodes.AsNoTracking().ToListAsync();            
        }

        private bool IsValid()
        {            
            if (string.IsNullOrEmpty(Employee.Error))
                return true;
            return false;
                
        }

        private async Task SaveNew(object parameter)
        {
            if (MessageBox.Show("Do you want to save new employee?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using var db = new AppDbContext();

                    db.Employees.Add(Employee.ToEmployeeEntity());
                    await db.SaveChangesAsync();
                    RefreshDataRequested?.Invoke();

                    var window = parameter as Window;                            
                    window.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }               
            }                       
        }

        private async Task EditExisted(object parameter)
        {
            if (MessageBox.Show("Do you want to edit employee information?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using var db = new AppDbContext();

                    db.Employees.Update(Employee.ToEmployeeEntity());
                    await db.SaveChangesAsync();
                    RefreshDataRequested?.Invoke();

                    var window = parameter as Window;                 
                    window.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}
