using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.Models
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int? user_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public byte[] image { get; set; }
        public string role { get; set; }
    }
}