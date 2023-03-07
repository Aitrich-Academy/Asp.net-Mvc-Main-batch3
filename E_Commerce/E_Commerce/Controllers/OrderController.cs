using DAL.Manager;
using DAL.Models;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace E_Commerce.Controllers
{
    [RoutePrefix("api/Order")]  // Url creation Route
    public class OrderController : ApiController
    {
        E_COMMERCEEntities db = new E_COMMERCEEntities();
        public string GetMe()
        {
            return "Hello...";
        }

        //api/sample/userDetailsByID?id=1
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpGet]
        //[Route("orderView")]
        //[HttpPost]
        //public ENT_Order orderView(string id)
        //{
        //    OrderManager odmngr = new OrderManager();
        //    ENT_Order return_Obj = new ENT_Order();
        //    Order odr_Obj = odmngr.orderDetails(Convert.ToInt32(id));
        //    if (odr_Obj != null)
        //    {
        //        return_Obj.order_id = odr_Obj.order_id;
        //        return_Obj.user_id = (int)odr_Obj.user_id;
        //        return_Obj.product_id = (int)odr_Obj.product_id;
        //        return_Obj.quantity = odr_Obj.quantity;
        //        return_Obj.total_price = odr_Obj.total_price;
        //        return_Obj.status = odr_Obj.status;
        //        return_Obj.createdBy = "system";
        //        return_Obj.createdDate = DateTime.Now.ToString();
        //        return_Obj.lastModifiedBy = "system";
        //        return_Obj.lastModifiedDate = DateTime.Now.ToString();
        //    }
        //    return return_Obj;
        //}

        //api/sample/allOrder (all orders list)
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("allOrder")]
        public List<Ent_Order> allOrder()
        {
            OrderManager odmngr = new OrderManager();
            List<Ent_Order> return_List = new List<Ent_Order>();
            List<Order> odr_Obj = odmngr.allOrders();
            if (odr_Obj.Count != 0)
            {
                foreach (var obj in odr_Obj)
                {
                    return_List.Add(new Ent_Order
                    {
                        order_id = obj.order_id,
                        user_id = obj.user_id,
                        product_id = obj.product_id,
                        quantity = obj.quantity,
                        total_price = obj.total_price,
                        status = obj.status,
                        createdBy = "system",
                        createdDate = DateTime.Now.ToString(),
                        lastModifiedBy = "system",
                        lastModifiedDate = DateTime.Now.ToString(),
                    });
                }
            }
            return return_List;
        }
        //api/sample/userDetailsByID?id=1 (User_Id single order)
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("orderViewUsrid")]
        [HttpPost]
        public Ent_Order orderViewUsrid(string id)
        {
            OrderManager odmngr = new OrderManager();
            Ent_Order return_Obj = new Ent_Order();
            Order odr_Obj = odmngr.orderdetUserId(Convert.ToInt32(id));

            if (odr_Obj != null)
            {
                return_Obj.order_id = odr_Obj.order_id;
                return_Obj.user_id = (int)odr_Obj.user_id;
                return_Obj.product_id = (int)odr_Obj.product_id;
                return_Obj.quantity = odr_Obj.quantity;
                return_Obj.total_price = odr_Obj.total_price;
                return_Obj.status = odr_Obj.status;
                return_Obj.createdBy = "system";
                return_Obj.createdDate = DateTime.Now.ToString();
                return_Obj.lastModifiedBy = "system";
                return_Obj.lastModifiedDate = DateTime.Now.ToString();
            }
            return return_Obj;
        }

        //api/sample/allUsers (same id with 2 order)
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("allOrderuser")]
        public List<Ent_Order> allOrderuser(int id)
        {
            OrderManager odmngr = new OrderManager();
            List<Ent_Order> return_List = new List<Ent_Order>();
            List<Order> odr_Obj = odmngr.allOrdersusr(Convert.ToInt32(id));
            if (odr_Obj.Count != 0)
            {
                foreach (var obj in odr_Obj)
                {
                    return_List.Add(new Ent_Order
                    {
                        order_id = obj.order_id,
                        user_id = obj.user_id,
                        product_id = obj.product_id,
                        quantity = obj.quantity,
                        total_price = obj.total_price,
                        status = obj.status,
                        createdBy = "system",
                        createdDate = DateTime.Now.ToString(),
                        lastModifiedBy = "system",
                        lastModifiedDate = DateTime.Now.ToString(),
                    });
                }
            }
            return return_List;
        }
        //api/sample/orderInsert
        //[System.Web.Http.AcceptVerbs("GET", "Post")]
        [System.Web.Http.HttpGet]
        [Route("orderInsert")] // Url creation Route
        [HttpPost]

        public string orderInsert(Ent_Order Obj)
        {
            OrderManager odrmngr = new OrderManager();

            ProductManager prmngr = new ProductManager();
            Ent_Order objodr = Obj;
            Order odr = new Order();
            Product pdr = new Product();

           var pr = db.Products.Where(e => e.product_id == Obj.product_id && e.status != "D").SingleOrDefault();

            // odr.order_id = objodr.order_id;
            odr.user_id = (int)objodr.user_id;
            odr.product_id = (int)objodr.product_id;
            odr.quantity = objodr.quantity;
            odr.total_price = (int)(objodr.quantity * pr.price);
            odr.status = "A";
            odr.createdBy = "system";
            odr.createdDate = DateTime.Now.ToString();
            odr.lastModifiedBy = "system";
            odr.lastModifiedDate = DateTime.Now.ToString();
            return odrmngr.Orderinsert(odr);
        }
        //api/sample/OrderUpdate (status set to D)
        [HttpPut]
        [Route("orderUpdate")]

        public HttpResponseMessage orderUpdate(int id, [FromBody] Ent_Order Obj)
        {
            OrderManager odrmngr = new OrderManager();
            Ent_Order entobj = Obj;
            Order odr = new Order();

            odr.order_id = id;
            //odr.user_id = (int)entobj.user_id;
            //odr.product_id = (int)entobj.product_id;
            //odr.quantity = entobj.quantity;
            //odr.total_price = entobj.total_price;
            odr.status = "D";
            //odr.createdBy = "system";
            //odr.createdDate = DateTime.Now.ToString();
            //odr.lastModifiedBy = "system";
            //odr.lastModifiedDate = DateTime.Now.ToString();
            odrmngr.OrderUpdate(odr);

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Updated Succesfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ERROR");
            }
        }

        //api/sample/Delete
        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage Delete(int id)
        {
            Order ord = new Order();
            OrderManager odmngr = new OrderManager();
            ord.order_id = id;
            ord.status = "D";
            odmngr.OrderCancelation(ord);

            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }

        //[Route("api/AjaxAPI/SendEmail")]
        [Route("SendEmail")]
        [HttpPost]
        public string SendEmail(Ent_Email email)
        {
            //Read SMTP section from Web.Config.
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage mm = new MailMessage(smtpSection.From, email.Email))
            {
                mm.Subject = email.Subject;
                mm.Body = email.Body;

                mm.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = smtpSection.Network.Host;
                    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = smtpSection.Network.Port;
                    smtp.Send(mm);
                }
            }

            return "Email sent sucessfully.";
        }
    }
}
