using DAL.Manager;
using DAL.Models;
//using E_Commerce_Project.Model;
using System;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;
using E_Commerce_Project.Utils;
using E_Commerce.Models;
using E_Commerce_Project.Models;
using System.Web.Http.Cors;

namespace E_Commerce_Project.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        UserManager mngr = new UserManager();

        [HttpGet]
        [Route("GetMe")]
        public string GetMe()
        {
            return "Hello develper, welcome to Sample App services.";
        }


        [Route("userRegistration")]
        [HttpPost]
        public string userRegistration(Ent_UserRegistration Objc)
        {

            Ent_UserRegistration objUser = (Ent_UserRegistration)Objc;
            User tbl_Obj = new User();
            tbl_Obj.name = objUser.name;
            tbl_Obj.email = objUser.email;
            tbl_Obj.phone = objUser.phone;
            tbl_Obj.address = objUser.address;
            tbl_Obj.password = objUser.password;
            //tbl_Obj.image = objUser.image;
            tbl_Obj.role = "User";
            tbl_Obj.status = "A";
            tbl_Obj.createdBy = "system";
            tbl_Obj.createdDate = DateTime.Now.ToString();
            tbl_Obj.lastModifiedBy = "system";
            tbl_Obj.lastModifiedDate = DateTime.Now.ToString();
            return mngr.userRegistration(tbl_Obj);

        }
        [HttpPut]
        [Route("userRegistrationupdate")]
        public string userRegistrationupdate(int id, Ent_UserRegistration Objc)
        {
            Ent_UserRegistration objUser = (Ent_UserRegistration)Objc;
            User ur = new User();

            ur.user_id = id;
            ur.name = objUser.name;
            ur.email = objUser.email;
            ur.phone = objUser.phone;
            ur.address = objUser.address;
            ur.image = objUser.image;
            ur.role = "User";
            ur.password = objUser.password;
            ur.createdBy = "system";
            ur.createdDate = DateTime.Now.ToString();
            ur.lastModifiedBy = "system";
            ur.lastModifiedDate = DateTime.Now.ToString();
            ur.status = "A";

            return mngr.userRegistration_update(ur);
        }

        [HttpDelete]
        [Route("userRegistrationdelete")]
        public HttpResponseMessage Delete(int id)
        {
            mngr.userRegistration_delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");
        }

        #region Get user details by id

      
        [HttpGet]
        [Route("userDetailsByID")]
       
        public Ent_UserRegistration userDetailsByID(string id)
        {
            UserManager mngr = new UserManager();
            Ent_UserRegistration tbl_Obj = new Ent_UserRegistration();
            User return_Obj = mngr.userDetails(int.Parse(id));

            if (return_Obj != null)
            {
                tbl_Obj.user_id = return_Obj.user_id;
                tbl_Obj.name = return_Obj.name;
                tbl_Obj.email = return_Obj.email;
                tbl_Obj.phone = return_Obj.phone;
                tbl_Obj.address = return_Obj.address;
                tbl_Obj.password = return_Obj.password;
                //tbl_Obj.image = objUser.image;
                tbl_Obj.role = "User";
                tbl_Obj.status = return_Obj.status;
                tbl_Obj.createdBy = "system";
                tbl_Obj.createdDate = DateTime.Now.ToString();
                tbl_Obj.lastModifiedBy = "system";
                tbl_Obj.lastModifiedDate = DateTime.Now.ToString();

            }
            return tbl_Obj;
        }

        #endregion


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("uploadFile")]
        public HttpResponseMessage UploadFile()
        {
            AuthenticationHeaderValue authorization = Request.Headers.Authorization;
            if (authorization == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    message = "Please Login"
                });
            }
            User usersDTO = TokenManager.ValidateToken(authorization.Parameter);
            if (usersDTO != null && usersDTO.user_id != null && usersDTO.role == "User")
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ?
              HttpContext.Current.Request.Files[0] : null;

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);


                

                    byte[] byteArr;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.InputStream.CopyTo(memoryStream);
                        byteArr = memoryStream.ToArray();
                    }
                    
                    User ur = new User();
                    //id needs to take from token
                    ur.user_id = 2;
                    ur.image = byteArr;
                    var res = mngr.userPatch(ur);
                    return Request.CreateResponse(HttpStatusCode.OK, res);

                }
                return null;
               

            }
            else
            {
               
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    message = "Please Login"
                });
            }

           
        }

        #region login
        
        [HttpGet]
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage login(Ent_UserRegistration a)
        {
           

            Ent_UserRegistration tbl_obj = (Ent_UserRegistration)a;
            UserManager mngr = new UserManager();
            User return_Obj = new User();

            if (tbl_obj != null)
            {
                return_Obj.email = tbl_obj.email;
                return_Obj.password = tbl_obj.password;
            }
            User result = mngr.login(return_Obj);

            if (result != null)
            {
                String token = TokenManager.GenerateToken(result);
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO();
                loginResponseDTO.Token = token;
                loginResponseDTO.email = result.email;
                loginResponseDTO.user_id = result.user_id;
                loginResponseDTO.address = result.address;
                loginResponseDTO.phone = result.phone;
                loginResponseDTO.role = result.role;
                loginResponseDTO.name = result.name;

                ResponseDataDTO response = new ResponseDataDTO(true, "Success", loginResponseDTO);
                return Request.CreateResponse(HttpStatusCode.OK, response);            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid User name and password !");
            }
        }
        #endregion
    }
}
