using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Ent_Order
    {
        public int order_id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public int total_price { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }
        //public int price { get; set; }
    }
}