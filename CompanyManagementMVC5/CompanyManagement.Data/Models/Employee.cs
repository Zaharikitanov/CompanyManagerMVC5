using System;

namespace CompanyManagement.Data.Models
{
    public class Employee : DbEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        public EmployeeExperienceLevel ExperienceLevel { get; set; }

        public string ProfileImage { get; set; }

        public Guid OfficeId { get; set; }

        public Guid CompanyId { get; set; }

        public Office Office { get; set; }
    }
}
