using Employees.Data.Db.Entities;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Employees
{
    public static class Helpers
    {        
        public static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static EmployeeModel ToEmployeeModel(this Employee employee)
        {
            return new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Position = employee.Position,
                Salary = employee.Salary,
                PhoneCode = employee.PhoneCode,
                PhoneNumber = employee.PhoneNumber,
                EmploymentDate = employee.EmploymentDate,
                DismissalDate = employee.DismissalDate
            };
        }

        public static Employee ToEmployeeEntity(this EmployeeModel employee)
        {
            return new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Position = employee.Position,
                Salary = employee.Salary,
                PhoneCode = employee.PhoneCode,
                PhoneNumber = employee.PhoneNumber,
                EmploymentDate = employee.EmploymentDate.Value,
                DismissalDate = employee.DismissalDate
            };
        }
    }
}
