using CompanyManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Services.Interfaces
{
    public interface IOfficeService
    {
        Task<Office> Create(Office office, Guid companyId);
        Task<Office> Delete(Guid id);
        Task<List<Office>> GetAll();
        Task<Office> GetOne(Guid id);
        Task<Office> Update(Office office);
    }
}