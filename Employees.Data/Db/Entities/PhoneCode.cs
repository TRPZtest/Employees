using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Data.Db.Entities
{
    public class PhoneCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
