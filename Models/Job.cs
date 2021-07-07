using System;
using System.Collections.Generic;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class Job
    {
        public Job()
        {
            JobEmployees = new HashSet<JobEmployee>();
            JobMaterials = new HashSet<JobMaterial>();
        }
        // Model properties 
        public int JobCardId { get; set; }
        public int CustomerId { get; set; }
        public int JobTypeId { get; set; }
        public int Days { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual ICollection<JobEmployee> JobEmployees { get; set; }
        public virtual ICollection<JobMaterial> JobMaterials { get; set; }
    }
}
