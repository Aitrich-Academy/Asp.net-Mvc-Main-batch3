using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models

{
    public class Ent_UserRegistration
    {
        public int? user_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public byte[] image { get; set; }
        public string role { get; set; }
        public string status { get; set; }
        public string password { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedDate { get; set; }

    }

}