using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class UserManager
    {
        E_COMMERCE1Entities usr = new E_COMMERCE1Entities();

        public string userRegistration(User Obj)
        {
            int result = 0;
            //var objUser = usr.Users
            var objUser = usr.Users.Where(e => e.email == Obj.email && e.name == Obj.name && e.status != "D").SingleOrDefault();
            if (objUser == null)
            {
                Obj.status = "A";
                usr.Users.Add(Obj);
                result = usr.SaveChanges();
            }
            else
            {
                objUser.name = Obj.name;
                objUser.email = Obj.email;
                objUser.phone = Obj.phone;
                objUser.address = Obj.address;
                objUser.image = Obj.image;
                objUser.role = Obj.role;
                objUser.password = Obj.password;
                objUser.status = Obj.status;
                objUser.createdBy = Obj.createdBy;
                objUser.createdDate = Obj.createdDate;
                objUser.lastModifiedBy = Obj.lastModifiedBy;
                objUser.lastModifiedDate = DateTime.Now.ToString();
                objUser.status = "A";
                usr.Entry(objUser).State = EntityState.Modified;
                result = usr.SaveChanges();
            }

            if (result > 0)
            {
                return Obj.user_id.ToString();
            }
            else
            {
                return "Error";
            }

        }
        public string userRegistration_update(User ur)
        {

            var objUser = usr.Users.Where(e => e.user_id == ur.user_id && e.status != "D").SingleOrDefault();
            if (objUser == null)
            {
                return "error";
            }
            else
            {
                objUser.name = ur.name;
                objUser.email = ur.email;
                objUser.phone = ur.phone;
                objUser.address = ur.address;
                objUser.image = ur.image;
                objUser.role = ur.role;
                objUser.password = ur.password;
                objUser.status = ur.status;
                objUser.createdBy = ur.createdBy;
                objUser.createdDate = ur.createdDate;
                objUser.lastModifiedBy = ur.lastModifiedBy;
                objUser.lastModifiedDate = DateTime.Now.ToString();
                objUser.status = "A";

                usr.Entry(objUser).State = EntityState.Modified;
                usr.SaveChanges();
                return objUser.user_id.ToString();
            }
        }

        public User userDetails(int Id)
        {
            User return_Obj = new User();
            return return_Obj = usr.Users.Where(e => e.user_id == Id && e.status != "D").SingleOrDefault();
        }
        public void userRegistration_delete(int id)
        {

            var objUser = usr.Users.Where(e => e.user_id == id && e.status != "D").SingleOrDefault();

            usr.Users.Remove(objUser);
            usr.SaveChanges();

        }

        public User login(User a)
        {
            try
            {
                var objUser = usr.Users.Where(e => e.email == a.email && e.password == a.password && e.status != "D").FirstOrDefault();
                return objUser;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public string userPatch(User ur)
        {

            var objUser = usr.Users.Where(e => e.user_id == ur.user_id && e.status != "D").SingleOrDefault();
            if (objUser == null)
            {
                return "error";
            }
            else
            {
                objUser.name = ur.name == null ? objUser.name : ur.name;
                objUser.email = ur.email == null ? objUser.email : ur.email;
                objUser.phone = ur.phone == null ? objUser.phone : ur.phone;
                objUser.address = ur.address == null ? objUser.address : ur.address;
                objUser.image = ur.image;
                objUser.role = ur.role == null ? objUser.role : ur.role; ;
                objUser.password = ur.password == null ? objUser.password : ur.password;
                objUser.status = ur.status == null ? objUser.status : ur.status;

                //objUser.lastModifiedBy = ur.lastModifiedBy;
                objUser.lastModifiedDate = DateTime.Now.ToString();


                usr.Entry(objUser).State = EntityState.Modified;
                usr.SaveChanges();
                return objUser.user_id.ToString();
            }
        }

    }
}
