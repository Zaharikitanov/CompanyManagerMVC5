using CompanyManagement.Data.Models;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Data.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly CompanyManagerContext _database;

        public OfficeService(CompanyManagerContext database)
        {
            _database = database;
        }

        public async Task<List<Office>> GetAll()
        {
            return await _database.Offices
                .Select(c => c)
                .ToListAsync();
        }

        public async Task<Office> GetOne(Guid id)
        {
            return await _database.Offices
                .Where(c => c.Id == id)
                .Select(c => c)
                .FirstOrDefaultAsync();
        }

        public async Task<Office> Create(Office office, Guid companyId)
        {
            office.CompanyId = companyId;
            office.Id = Guid.NewGuid();
            _database.Offices.Add(office);
            await _database.SaveChangesAsync();
            return office;
        }

        public async Task<Office> Update(Office office)
        {
            var existing = _database.Offices
                .Where(c => c.Id == office.Id)
                .Select(c => c)
                .FirstOrDefault();

            if (existing != null)
            {
                existing.City = office.City;
                existing.Country = office.Country;
                existing.Street = office.Street;
                existing.StreetNumber = office.StreetNumber;
                existing.IsHeadquarters = office.IsHeadquarters;
                await _database.SaveChangesAsync();
            }

            return office;
        }

        public async Task<Office> Delete(Guid id)
        {
            var office = _database.Offices.Find(id);
            _database.Offices.Remove(office);
            await _database.SaveChangesAsync();

            return office;
        }
    }
}
