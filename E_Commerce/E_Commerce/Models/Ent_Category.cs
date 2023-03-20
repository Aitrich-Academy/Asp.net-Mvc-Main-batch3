using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Ent_Category
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        public string  image { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}