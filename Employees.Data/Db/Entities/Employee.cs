using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Data.Db.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public Double Salary { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneCode PhoneCode { get; set; }
        [Required]
        public int PhoneCodeId { get; set; }
        [Required]
        public DateOnly EmploymentDate { get; set; }
        public DateOnly? DismissalDate { get; set; }

    }
}
