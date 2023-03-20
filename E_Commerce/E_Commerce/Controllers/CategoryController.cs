using DAL.Manager;
using DAL.Models;
using E_Commerce.Models;
using E_Commerce_Project.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {

        [HttpGet]
        [Route("demo")]
        public string demo()
        {
            return "HII";
        }

        //[HttpGet]
        //[AcceptVerbs("GET", "POST")]
        [Route("CategoryInsert")]
        [HttpPost]
        public HttpResponseMessage CategoryInsert(Ent_Category Obj)
        {
            AuthenticationHeaderValue authorization = Request.Headers.Authorization;
            if (authorization != null)
            {
                User usersDTO = TokenManager.ValidateToken(authorization.Parameter);

                if (usersDTO.user_id != null && usersDTO.role == "Admin")
                {
                    CategoryManager mngr = new CategoryManager();
                    Ent_Category objCat = Obj;
                    Category tbl_Obj = new Category();
                    tbl_Obj.category_name = objCat.name;
                    tbl_Obj.description = objCat.description;
                    tbl_Obj.image = Encoding.ASCII.GetBytes(objCat.image);
                    tbl_Obj.status = "A";
                    tbl_Obj.createdBy = "admin";
                    tbl_Obj.createdDate = DateTime.Now.ToString();
                    tbl_Obj.lastModifiedBy = "admin";
                    tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
                    return Request.CreateResponse(HttpStatusCode.OK, mngr.InsertCategory(tbl_Obj)); 
                }
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Login");
        }

        [HttpPut]
        [Route("CategoryUpdate")]
        public HttpResponseMessage CategoryUpdate(int id,[FromBody] Ent_Category Obj)
        {
            CategoryManager mngr=new CategoryManager();
            Ent_Category ObjCat= Obj;
            Category tbl_Obj = new Category();
            tbl_Obj.category_id = id;
            tbl_Obj.category_name= ObjCat.name;
            tbl_Obj.description= ObjCat.description;
            tbl_Obj.image = Encoding.ASCII.GetBytes(ObjCat.image);
            tbl_Obj.status = ObjCat.status;
            tbl_Obj.createdBy = ObjCat.createdBy;
            tbl_Obj.createdDate = DateTime.Now.ToString();  
            tbl_Obj.lastModifiedBy = ObjCat.lastModifiedBy;
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            mngr.UpdateCategory(tbl_Obj);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,"Updated Successfully");
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Error");
            }
           
        }

        [HttpGet]
        [Route("allcategory")]
        public List<Ent_Category> allcategory(string id = null)
        {
            CategoryManager mngr = new CategoryManager();
            List<Ent_Category> List = new List<Ent_Category>();
            List<Category> tbl_obj = mngr.GetAllCategory(Convert.ToInt32(id));
            if (tbl_obj.Count != 0)
            {

                foreach (var obj in tbl_obj)
                {
                    Ent_Category entCat = new Ent_Category();
                    entCat.id = obj.category_id;
                    entCat.name = obj.category_name;
                    entCat.description = obj.description;
                    if(obj.image!=null)
                    entCat.image = Encoding.ASCII.GetString(obj.image);

                    List.Add(entCat);
                    //List.Add(new Ent_Category
                    //{
                    //    id = obj.category_id,
                    //    name = obj.category_name,
                    //    description = obj.description,
                    //    image = Encoding.ASCII.GetString(obj.image),
                    //    //status = obj.status,
                    //    //createdBy = obj.createdBy,
                    //    //createdDate = obj.createdDate,
                    //    //lastModifiedBy = obj.lastModifiedBy,
                    //    //lastModifiedDate = obj.lastModifiedDate
                    //    //lastModifiedDate = Convert.ToDateTime(obj.lastModified).ToShortDateString(),
                    //}) ;
                }
            }
            return List;
        }

        [HttpGet]
        [Route("categorybyid")]
        public Ent_Category categoryByID(string id)
        {
            CategoryManager mngr = new CategoryManager();
            Ent_Category entcatObj = new Ent_Category();
            Category tbl_obj = mngr.categorybyid(Convert.ToInt32(id));

            if (tbl_obj != null)
            {
                entcatObj.id = tbl_obj.category_id;
                entcatObj.name = tbl_obj.category_name;
                entcatObj.description = tbl_obj.description;
                entcatObj.image = Encoding.ASCII.GetString(tbl_obj.image);
                entcatObj.status = tbl_obj.status;
                entcatObj.createdBy = tbl_obj.createdBy;
                entcatObj.createdDate = tbl_obj.createdDate;
                entcatObj.lastModifiedBy = tbl_obj.lastModifiedBy;
                entcatObj.lastModifiedDate = tbl_obj.lastModifiedDate;
            }
            return entcatObj;
        }

        [HttpDelete]
        [Route("CategoryDelete")]
        public HttpResponseMessage CategoryDelete(int id)
        {
            CategoryManager mngr=new CategoryManager();
            mngr.deletecategory(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Category Deleted Succesfully");
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
                    HttpContext.Current.Server.MapPath("~/uploads"),
                    fileName
                );

                file.SaveAs(path);
            }

            return file != null ? "/uploads/" + file.FileName : null;
        }

    }
}