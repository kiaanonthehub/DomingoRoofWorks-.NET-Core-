using System;
using System.Collections.Generic;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class JobMaterial
    {
        public int JobMaterialId { get; set; }
        public int JobCardId { get; set; }
        public int MaterialId { get; set; }
        public int Quantity { get; set; }

        public virtual Job JobCard { get; set; }
        public virtual Material Material { get; set; }
    }
}
