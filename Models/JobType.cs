using System;
using System.Collections.Generic;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class JobType
    {
        public JobType()
        {
            Jobs = new HashSet<Job>();
        }

        // Model properties 
        public int JobTypeId { get; set; }
        public string JobType1 { get; set; }
        public decimal Rate { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
