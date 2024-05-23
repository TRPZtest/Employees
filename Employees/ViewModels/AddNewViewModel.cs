using Employees.Data.Db.Entities;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ViewModels
{
    class AddNewViewModel
    {
        public List<PhoneCode> PhoneCodes { get; set; }
        public EmployeeModel Employee { get; set; } = new EmployeeModel();
        public AddNewViewModel() 
        {
            PhoneCodes = new List<PhoneCode>() 
            { 
                new PhoneCode() { Id = 1, Code = "+380" },
                new PhoneCode() {Code = "+48" }             
            };         
        }   
    }
}
