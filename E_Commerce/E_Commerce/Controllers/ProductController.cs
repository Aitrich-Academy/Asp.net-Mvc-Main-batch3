using DAL.Manager;
using DAL.Models;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("demo")]
        public string demo()
        {
            return "hii";
        }

        [HttpPost   ]
        [Route("ProductInsert")]
        public string ProductInsert(Ent_Product Obj)
        {
            ProductManager mngr=new ProductManager();
            Ent_Product entProObj = Obj;
            Products tbl_Prod= new Products();

            tbl_Prod.product_name = entProObj.name;
            tbl_Prod.category_id = entProObj.categoryId;
            tbl_Prod.description = entProObj.description;
            tbl_Prod.stock = entProObj.stock;
            tbl_Prod.image = entProObj.image;
            tbl_Prod.price= entProObj.price;
            tbl_Prod.status = "A";
            tbl_Prod.createdBy = "admin";
            tbl_Prod.createdDate = DateTime.Now.ToString();
            tbl_Prod.lastModifiedBy = "admin";
            tbl_Prod.lastModifiedDate = DateTime.Now.ToString();
            return mngr.InsertProduct(tbl_Prod);
        }

        [HttpPut]
        [Route("ProductUpdate")]
        public HttpResponseMessage ProductUpdate(int id,[FromBody] Ent_Product Obj)
        {
            
            ProductManager mngr = new ProductManager();
            Ent_Product entProObj = Obj;
            Products tbl_Prod = new Products();

            tbl_Prod.product_id = id;
            tbl_Prod.product_name=entProObj.name;
            tbl_Prod.category_id = entProObj.categoryId;
            tbl_Prod.description = entProObj.description;
            tbl_Prod.stock = entProObj.stock;
            tbl_Prod.image = entProObj.image;
            tbl_Prod.status = entProObj.status;
            tbl_Prod.price = entProObj.price;
            tbl_Prod.createdBy = "admin";
            tbl_Prod.createdDate = DateTime.Now.ToString();
            tbl_Prod.lastModifiedBy = "admin";
            tbl_Prod.lastModifiedDate = DateTime.Now.ToString();
            mngr.UpdateProduct(tbl_Prod);

            try
            {
                return  Request.CreateResponse(HttpStatusCode.OK,"Updated Succesfully");
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ERROR");
            }
        }

        [HttpGet]
        [Route("allProducts")]
        public List<Ent_Product> allProducts()
        {
            ProductManager mngr=new ProductManager();
            List<Ent_Product> list= new List<Ent_Product>();
            List<Products> tbl_Obj = mngr.GetAllProoducts();

            foreach(var obj in tbl_Obj)
            {
                list.Add(new Ent_Product
                {
                    id=obj.product_id,
                    name=obj.product_name,
                    categoryId=obj.category_id,
                    description=obj.description,
                    stock=obj.stock,
                    image=obj.image,
                    status=obj.status,
                    price=Convert.ToInt32(obj.price),
                    createdBy=obj.createdBy,
                    createdDate=obj.createdDate,
                    lastModifiedBy=obj.lastModifiedBy,
                    lastModifiedDate=obj.lastModifiedDate
                });
            }
            return list;
        }

        [HttpGet]
        [Route("GetProductsByCategory")]
        public List<Ent_Product> GetProductsByCategory(int cid)
        {
            ProductManager mngr = new ProductManager();
            List<Ent_Product> list = new List<Ent_Product>();
            List<Products> tbl_Obj = mngr.ProductsByCategory(cid);

            foreach (var obj in tbl_Obj)
            {
                list.Add(new Ent_Product
                {
                    id = obj.product_id,
                    name = obj.product_name,
                    categoryId = obj.category_id,
                    description = obj.description,
                    stock = obj.stock,
                    image = obj.image,
                    status = obj.status,
                    price=Convert.ToInt32(obj.price),
                    createdBy = obj.createdBy,
                    createdDate = obj.createdDate,
                    lastModifiedBy = obj.lastModifiedBy,
                    lastModifiedDate = obj.lastModifiedDate
                });
            }
            return list;
        }


        [HttpGet]
        [Route("ProductById")]
        public Ent_Product ProductById(int id)
        {
            ProductManager mngr=new ProductManager();
            Ent_Product entProdObj=new Ent_Product();
            Products tbl_prod = mngr.GetProductById(id);

            if(tbl_prod != null)
            {
                entProdObj.id = tbl_prod.product_id;
                entProdObj.name = tbl_prod.product_name;
                entProdObj.categoryId = tbl_prod.category_id;
                entProdObj.description = tbl_prod.description;
                entProdObj.stock = tbl_prod.stock;
                entProdObj.image = tbl_prod.image;
                entProdObj.status = tbl_prod.status;
                entProdObj.price = Convert.ToInt32(tbl_prod.price);
                entProdObj.createdBy = tbl_prod.createdBy;
                entProdObj.createdDate = tbl_prod.createdDate;
                entProdObj.lastModifiedBy = tbl_prod.lastModifiedBy;
                entProdObj.lastModifiedDate = tbl_prod.lastModifiedDate;
            }
            return entProdObj;
        }

        [HttpGet]
        [Route("ProductSearch")]
        public List<Ent_Product> ProductByName(string proName)
        {
            ProductManager mngr = new ProductManager();
            List<Ent_Product> list = new List<Ent_Product>();
            List<Products> tbl_prod = mngr.ProductSearch(proName);

            foreach(var obj in tbl_prod)
            {
                list.Add(new Ent_Product
                {
                    id= obj.product_id,
                    name = obj.product_name,
                    categoryId = obj.category_id,
                    description = obj.description,
                    stock = obj.stock,
                    image = obj.image,
                    status = obj.status,
                    price = Convert.ToInt32(obj.price),
                    createdBy = obj.createdBy,
                    createdDate = obj.createdDate,
                    lastModifiedBy = obj.lastModifiedBy,
                    lastModifiedDate = obj.lastModifiedDate
                });
            }
            return list;
        }


        [HttpDelete]
        [Route("ProductDelete")]
        public HttpResponseMessage ProductDelete(int id)
        {
            ProductManager mngr = new ProductManager();
            mngr.DeleteProduct(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Product Deleted Succesfully");
        }

        [HttpPost]
        [Route("uploadFile")]
        public string UploadFile()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/images"),
                    fileName
                );

                file.SaveAs(path);
            }

            return file != null ? "/images/" + file.FileName : null;
        }



    }
}