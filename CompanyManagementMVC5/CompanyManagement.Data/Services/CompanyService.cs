using CompanyManagement.Data.Models;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly CompanyManagerContext _database;

        public CompanyService(CompanyManagerContext database)
        {
            _database = database;
        }

        public async Task<List<Company>> GetAll()
        {
            return await _database.Companies
                .Select(c => c)
                .ToListAsync();
        }

        public async Task<Company> GetOne(Guid id)
        {
            var company = await _database.Companies
                .Where(c => c.Id == id)
                .Select(c => c)
                .FirstOrDefaultAsync();
            company.Offices = await _database.Offices
                .Where(o => o.CompanyId == id)
                .ToListAsync();
            return company;
        }

        public async Task<Company> Create(Company company)
        {
            company.Id = Guid.NewGuid();
            company.CreatedAt = DateTime.Now;
            _database.Companies.Add(company);
            await _database.SaveChangesAsync();
            return company;
        }

        public async Task<Company> Update(Company company)
        {
            var existing = _database.Companies
                .Where(c => c.Id == company.Id)
                .Select(c => c)
                .FirstOrDefault();

            if (existing != null)
            {
                existing.Name = company.Name;
                await _database.SaveChangesAsync();
            }

            return company;
        }

        public async Task<Company> Delete(Guid id)
        {
            var company = _database.Companies.Find(id);
            _database.Companies.Remove(company);
            await _database.SaveChangesAsync();

            return company;
        }
    }
}
