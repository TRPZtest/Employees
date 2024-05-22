using Employees.Data.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Data.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PhoneCode> PhoneCodes { get; set; }
        public 
        AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
