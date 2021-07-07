using System;
using System.Collections.Generic;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class Material
    {
        public Material()
        {
            JobMaterials = new HashSet<JobMaterial>();
        }

        // Model properties 
        public int MaterialId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<JobMaterial> JobMaterials { get; set; }
    }
}
