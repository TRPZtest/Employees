using Employees.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Employees.ViewModels
{
    class EmployeeViewModel
    {
        public IList<EmployeeModel> EmployeesList { get; set; } = new List<EmployeeModel>();
        public EmployeeModel SelectedEmployee {  get; set; }

        public EmployeeViewModel()
        {
            //EmployeesList = new ObservableCollection<Employee>()
            //{
            //    new Employee{ Id = 1 , FirstName = "Dima" },
            //    new Employee{ Id = 1 , FirstName = "John" }
            //};
        }      
    }
    
}
