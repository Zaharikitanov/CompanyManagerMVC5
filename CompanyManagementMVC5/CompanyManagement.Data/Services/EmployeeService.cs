using CompanyManagement.Data.Models;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            employee.Id = Guid.NewGuid();
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

        public async Task<Employee> Delete(Guid id)
        {
            var employee = _database.Employees.Find(id);
            _database.Employees.Remove(employee);
            await _database.SaveChangesAsync();

            return employee;
        }
    }
}
