using Employees.Data.Db.Entities;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Employees.Models
{
    public class EmployeeModel : BindableBase, IDataErrorInfo
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Patronymic { get; set; }     
        public string FullName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public PhoneCode? PhoneCode { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public DateTime? EmploymentDate { get; set; }      
        public DateTime? DismissalDate { get; set; }

        public string FullPhoneNumber => PhoneCode?.Code + PhoneNumber;

        public string Error
        {
            get
            {
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, new ValidationContext(this), validationResults);
                if (validationResults.Any(x => !string.IsNullOrEmpty(x.ErrorMessage)))
                    return "Validation error";
                else return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults))
                    return null;

               return validationResults.First().ErrorMessage;
            }
        }
    }
}
