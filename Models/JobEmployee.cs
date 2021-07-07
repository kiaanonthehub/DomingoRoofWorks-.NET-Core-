using System;
using System.Collections.Generic;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class JobEmployee
    {
        // Model properties 
        public int JobEmployeeId { get; set; }
        public int JobCardId { get; set; }
        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Job JobCard { get; set; }
    }
}
