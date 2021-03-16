using CompanyManagement.Data.Models;
using System.Data.Entity;

namespace CompanyManagement.Data
{
    public class CompanyManagerContext : DbContext
    {
         public DbSet<Company> Companies { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Offices)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.CompanyId);

            modelBuilder.Entity<Office>()
                .HasMany(c => c.EmployeesList)
                .WithRequired(e => e.Office)
                .HasForeignKey(e => e.OfficeId);
        }
    }
}
