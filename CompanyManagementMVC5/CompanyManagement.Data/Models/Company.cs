using System;
using System.Collections.Generic;

namespace CompanyManagement.Data.Models
{
    public class Company : DbEntity
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Office> Offices { get; set; } = new List<Office>();
        
    }
}
