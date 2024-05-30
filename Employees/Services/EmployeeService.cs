using EFCore.BulkExtensions;
using Employees.Data.Db;
using Employees.Data.Db.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Services
{
    public class EmployeeService : IDisposable
    {
        private readonly AppDbContext _dbContext;

        public EmployeeService() 
        {
            _dbContext = new AppDbContext();
 
        }
        public async Task<List<Employee>> GetAsync(string fullName = "")
        {          
            var employeesQuery = _dbContext.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
                employeesQuery = employeesQuery.Where(x => EF.Functions.Like(x.FullName, $"%{fullName}%")).AsQueryable();

            var employees = await employeesQuery
                .Include(x => x.PhoneCode)
                .AsNoTracking()
                .ToListAsync();

            return employees;
        }

        public async Task DeleteById(int id)
        {         
            await _dbContext.Employees.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<PhoneCode>> GetPhoneCodesAsync()
        {          
            var codes = await _dbContext.PhoneCodes.AsNoTracking().ToListAsync();

            return codes;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<Employee> employees)
        {
            await _dbContext.AddRangeAsync(employees);
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
