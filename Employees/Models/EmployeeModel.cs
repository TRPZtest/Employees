﻿using Employees.Data.Db.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Employees.Models
{
    public class EmployeeModel : BindableBase, IDataErrorInfo
    {         
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public PhoneCode PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public DateTime? EmploymentDate {  get; set; }
        public DateTime? DismissalDate {  get; set; }
        



        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrEmpty(FirstName))
                            return "First name is required";
                        break;
                    case nameof(SecondName):
                        if (string.IsNullOrEmpty(SecondName))
                            return "Second name is required";
                        break;
                    case nameof(PhoneCode):
                        if (PhoneCode is null)
                            return "Phone country code is required";
                        break;
                    case nameof(PhoneNumber):
                        if (string.IsNullOrEmpty(PhoneNumber))
                            return "Phone is required";
                        break;
                    case nameof(Salary):
                        if (Salary == 0)
                            return "Salary must be not a zero";
                        break;
                    case nameof(EmploymentDate):
                        if (EmploymentDate > DismissalDate && DismissalDate != null)
                            return "Dissmissale date must be later then employment date";
                        break;
                }
                return error;
            }
        }
    }
}
