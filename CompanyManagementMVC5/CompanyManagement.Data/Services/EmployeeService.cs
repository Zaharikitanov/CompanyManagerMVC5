using CompanyManagement.Data.Models;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CompanyManagerContext _database;

        public EmployeeService(CompanyManagerContext database)
        {
            _database = database;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _database.Employees
                .Select(c => c)
                .ToListAsync();
        }

        public async Task<Employee> GetOne(Guid id)
        {
            return await _database.Employees
                .Where(c => c.Id == id)
                .Select(c => c)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> Create(Employee employee)
        {
            if (employee.StartingDate == DateTime.MinValue)
            {
                employee.StartingDate = DateTime.Now;
            }
            _database.Employees.Add(employee);
            await _database.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Update(Employee employee)
        {
            _database.Entry(employee).State = EntityState.Modified;
            await _database.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> Delete(Guid id, string profileImagePath)
        {
            var employee = _database.Employees.Find(id);
            if (!string.IsNullOrEmpty(profileImagePath))
            {
                File.Delete(profileImagePath);
            }
            _database.Employees.Remove(employee);
            await _database.SaveChangesAsync();

            return employee;
        }
    }
}
