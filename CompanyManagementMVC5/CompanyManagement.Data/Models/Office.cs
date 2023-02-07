using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.Data.Models
{
    public class Office : DbEntity
    {
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        [Display(Name = "Street Number")]
        public int StreetNumber { get; set; }

        [Display(Name = "Headquarters")]
        public bool IsHeadquarters { get; set; }

        public string Documents { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
