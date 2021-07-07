using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomingoRoofWorks.Models
{
    public class Invoice
    {
        // Model properties 
        public virtual Job Jobs { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual Material Material { get; set; }
        public virtual JobMaterial Job_Material { get; set; }
        public virtual JobEmployee Job_Employee { get; set; }

    }
}
