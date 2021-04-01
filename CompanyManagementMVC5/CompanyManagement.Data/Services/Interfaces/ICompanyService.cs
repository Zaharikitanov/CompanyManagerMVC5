using CompanyManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> Create(Company company);
        Task<Company> Delete(Guid id);
        Task<List<Company>> GetAll();
        Task<Company> GetOne(Guid id);
        Task<Company> Update(Company company);
    }
}