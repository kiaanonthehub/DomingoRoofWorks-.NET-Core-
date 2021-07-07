using System;
using System.Collections.Generic;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class Employee
    {
        public Employee()
        {
            JobEmployees = new HashSet<JobEmployee>();
        }

        // Model properties 
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<JobEmployee> JobEmployees { get; set; }
    }
}
