using CompanyManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> Create(Employee employee);
        Task<Employee> Delete(Guid id, string profileImagePath);
        Task<List<Employee>> GetAll();
        Task<Employee> GetOne(Guid id);
        Task<Employee> Update(Employee employee);
    }
}