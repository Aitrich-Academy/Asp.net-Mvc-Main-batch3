﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Ent_Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public int stock { get; set; }
        public string image { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }
        public int price { get; set; }  

    }
}
