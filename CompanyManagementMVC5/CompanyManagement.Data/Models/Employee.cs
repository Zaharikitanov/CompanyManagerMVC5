using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.Data.Models
{
    public class Employee : DbEntity
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Starting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Vacation Days")]
        public int VacationDays { get; set; }

        [Display(Name = "Experience Level")]
        public EmployeeExperienceLevel ExperienceLevel { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImage { get; set; }

        public Guid OfficeId { get; set; }

        public Guid CompanyId { get; set; }

        public Office Office { get; set; }
    }
}
