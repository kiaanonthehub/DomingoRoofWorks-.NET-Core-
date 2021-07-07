using System;
using System.Collections.Generic;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Jobs = new HashSet<Job>();
        }

        // Model properties 
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
