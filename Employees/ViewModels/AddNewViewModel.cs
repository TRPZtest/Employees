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
    class AddNewViewModel
    {
        public ICommand SaveButtonCommand { get; set; }

        public List<PhoneCode> PhoneCodes { get; set; }
        public EmployeeModel Employee { get; set; } = new EmployeeModel();
        public AddNewViewModel() 
        {
            LoadDataAsync();       
        }   
            
        private async Task LoadDataAsync()
        {
            using var db = new AppDbContext();
            PhoneCodes = await db.PhoneCodes.ToListAsync();
            SaveButtonCommand = new RelayCommand(async o => await SaveNew(), o => IsValid());        
        }

        private bool IsValid()
        {            
            if (string.IsNullOrEmpty(Employee.Error))
                return true;
            return false;
                
        }

        private async Task SaveNew()
        {
            if (MessageBox.Show("Do you want to save new employee?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using var db = new AppDbContext();

                    db.Employees.Add(Employee.ToEmployeeEntity());
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }               
            }                       
        }
    }
}
