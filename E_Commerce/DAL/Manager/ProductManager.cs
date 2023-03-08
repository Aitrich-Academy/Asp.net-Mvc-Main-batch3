using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class ProductManager
    {
        E_COMMERCEEntities1 db =new E_COMMERCEEntities1();

        public string InsertProduct(Products tbl_prod)
        {
            int result = 0;

            var objUser=db.Products.Where(e=>e.product_name==tbl_prod.product_name && e.status!="D").SingleOrDefault();
            if(objUser==null)
            {
                tbl_prod.status = "A";
                db.Products.Add(tbl_prod);
                result=db.SaveChanges();
            }
            else
            {
                objUser.product_name= tbl_prod.product_name;
                objUser.category_id = tbl_prod.category_id;
                objUser.description = tbl_prod.description;
                objUser.stock = tbl_prod.stock;
                objUser.image = tbl_prod.image;
                objUser.status = "A";
                objUser.price = Convert.ToInt32(tbl_prod.price);
                objUser.createdBy = "admin";
                objUser.createdDate = DateTime.Now.ToString();
                objUser.lastModifiedBy = "admin";
                objUser.lastModifiedDate = DateTime.Now.ToString();
                db.Entry(tbl_prod).State=EntityState.Modified;
                db.SaveChanges();
            }
            if (result > 0)
            {
                return tbl_prod.product_id.ToString();
            }
            else
            {
                return "ERROR";
            }
        }

        public void UpdateProduct(Products tbl_Prod)
        {
            var objUser = db.Products.Where(e => e.product_id == tbl_Prod.product_id && e.status!="D").SingleOrDefault();

            if(objUser != null)
            {
                objUser.product_name= tbl_Prod.product_name;
                objUser.category_id = tbl_Prod.category_id;
                objUser.description = tbl_Prod.description;
                objUser.stock = tbl_Prod.stock;
                objUser.price = Convert.ToInt32(tbl_Prod.price);
                objUser.image = tbl_Prod.image;
                objUser.status="A";
                //objUser.createdBy = tbl_Prod.product_name;
                //objUser.createdDate = tbl_Prod.product_name;
                objUser.lastModifiedBy = "admin";
                objUser.lastModifiedDate = DateTime.Now.ToString();
                db.Entry(objUser).State=EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<Products> GetAllProoducts()
        {
            return db.Products.Where(e => e.status != "D").ToList(); 
        }

        public Products GetProductById(int pid)
        {
            Products tbl_Prod=new Products();
            return db.Products.Where(e => e.product_id == pid && e.status != "D").SingleOrDefault();       
        }




        public List<Products> ProductSearch(string proName)
        {
            //if((proName)!=null)
            //{
            //    return db.Products.Where(e=>e.product_name==proName && e.status!="D").SingleOrDefault();
            //}
            //else
            //{
            //    return db.Products.Where(e => e.status != "D").SingleOrDefault();
            //}
            try
            {
               
                return db.Products.Where(e=>e.product_name.Contains(proName) && e.status!="D").ToList();
            }
            catch (Exception)
            {

                throw;
            }
           

        }





        public void DeleteProduct(int id)
        {
            var ObjUser=db.Products.Where(e=>e.product_id== id && e.status!="D").SingleOrDefault();
            db.Products.Remove(ObjUser);
            db.SaveChanges();
        }

        public List<Products> ProductsByCategory(int cid)
        {
            if (cid != 0)
            {
                return db.Products.Where(e=>e.category_id==cid && e.status!="D").ToList();
            }
            else
            {
                return db.Products.Where(e => e.status != "D").ToList();
            }
        }

    }
}
