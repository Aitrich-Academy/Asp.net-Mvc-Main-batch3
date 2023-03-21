using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class CategoryManager
    {
        E_COMMERCE1Entities db = new E_COMMERCE1Entities();
        public string InsertCategory(Category tbl_cat)
        {
            int result = 0;

            var objuser = db.Categories.Where(e => e.category_name == tbl_cat.category_name && e.status != "D").SingleOrDefault();
            if (objuser == null)
            {
                tbl_cat.status = "A";
                db.Categories.Add(tbl_cat);
                result = db.SaveChanges();
            }
            else
            {
                objuser.category_name = tbl_cat.category_name;
                objuser.description = tbl_cat.description;
                objuser.image = tbl_cat.image;
                objuser.status = "A";
                objuser.createdBy = "admin";
                objuser.createdDate = DateTime.Now.ToString();
                objuser.lastModifiedBy = "admin";
                objuser.lastModifiedDate = DateTime.Now.ToString();
                db.Entry(objuser).State = EntityState.Modified;
                result=db.SaveChanges();
            }

            if (result > 0)
            {
                return tbl_cat.category_id.ToString();
            }
            else
            {
                return "ERROR";
            }
        }
        public void UpdateCategory(Category tbl_Cat)
        {
            var objUser=db.Categories.Where(e=>e.category_id == tbl_Cat.category_id && e.status!="D").SingleOrDefault();

            if (objUser != null) 
            {
                objUser.category_name= tbl_Cat.category_name;
                objUser.description= tbl_Cat.description;
                objUser.image = tbl_Cat.image;
                objUser.status = "A";
                //objUser.createdBy = "admin";
                //objUser.createdDate = DateTime.Now.ToString();
                objUser.lastModifiedBy = "admin";
                objUser.lastModifiedDate = DateTime.Now.ToString();
                db.Entry(objUser).State = EntityState.Modified; 
                db.SaveChanges();
            }
        }

        public List<Category> GetAllCategory(int id=0)
        {
            if(id!=0)
            {
                return db.Categories.Where(e=>e.category_id==id && e.status!="D").ToList();
            }
            else
            {
                return db.Categories.Where(e=>e.status!="D").ToList();
            }
        }

        public Category categorybyid(int id)
        {
            Category  tbl_cat= new Category();
            return db.Categories.Where(e=>e.category_id==id&&e.status!="D").SingleOrDefault();
        }

        public void deletecategory(int id)
        {
            var catId=db.Categories.Where(e=>e.category_id== id && e.status!="D").SingleOrDefault();
            db.Categories.Remove(catId);
            db.SaveChanges();
        }


    }
}
