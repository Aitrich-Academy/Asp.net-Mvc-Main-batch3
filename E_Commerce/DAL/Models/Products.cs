//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int category_id { get; set; }
        public string description { get; set; }
        public int stock { get; set; }
        public byte[] image { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }
        public Nullable<int> price { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}