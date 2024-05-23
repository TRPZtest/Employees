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
        
        public AppDbContext() : base()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=D:\db.sqlite;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for PhoneCode
            modelBuilder.Entity<PhoneCode>().HasData(
                new PhoneCode { Id = 1, Code = "+38" },
                new PhoneCode { Id = 2, Code = "+48" }               
            );

            // Seed data for Employee
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Patronymic = "A.",
                    BirthDate = new DateTime(1980, 1, 1),
                    Position = "Manager",
                    PhoneNumber = "1234567890",
                    PhoneCodeId = 1,
                    EmploymentDate = new DateOnly(2010, 5, 1)
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Patronymic = "B.",
                    BirthDate = new DateTime(1990, 2, 2),
                    Position = "Developer",
                    PhoneNumber = "0987654321",
                    PhoneCodeId = 2,
                    EmploymentDate = new DateOnly(2015, 6, 15)
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Patronymic = "C.",
                    BirthDate = new DateTime(1985, 3, 3),
                    Position = "Designer",
                    PhoneNumber = "1112223333",
                    PhoneCodeId = 1,
                    EmploymentDate = new DateOnly(2018, 7, 20)
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Bob",
                    LastName = "Brown",
                    Patronymic = "D.",
                    BirthDate = new DateTime(1975, 4, 4),
                    Position = "Analyst",
                    PhoneNumber = "4445556666",
                    PhoneCodeId = 1,
                    EmploymentDate = new DateOnly(2020, 8, 10)
                },
                new Employee
                {
                    Id = 5,
                    FirstName = "Charlie",
                    LastName = "Davis",
                    Patronymic = "E.",
                    BirthDate = new DateTime(1995, 5, 5),
                    Position = "Consultant",
                    PhoneNumber = "7778889999",
                    PhoneCodeId = 2,
                    EmploymentDate = new DateOnly(2022, 9, 5)
                }
            );
        }
    }
}
