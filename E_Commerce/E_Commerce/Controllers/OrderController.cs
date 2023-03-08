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
using Ent_Email = DAL.Manager.Ent_Email;
//using Ent_Email = DAL.Manager.Ent_Email;

namespace E_Commerce.Controllers
{
    [RoutePrefix("api/Order")]  // Url creation Route
    public class OrderController : ApiController
    {
        E_COMMERCEEntities1 db = new E_COMMERCEEntities1();


        OrderManager mgr = new OrderManager();
        UserManager userManager = new UserManager();
        MailManager mailMngr = new MailManager();
        Ent_Email mail = new Ent_Email();
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
            List<Orders> odr_Obj = odmngr.allOrders();
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
            Orders odr_Obj = odmngr.orderdetUserId(Convert.ToInt32(id));

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
            List<Orders> odr_Obj = odmngr.allOrdersusr(Convert.ToInt32(id));
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
        //[System.Web.Http.HttpGet]
        //[Route("orderInsert")] // Url creation Route
        //[HttpPost]

        //public string orderInsert(Ent_Order Obj)
        //{
        //    OrderManager odrmngr = new OrderManager();

        //    ProductManager prmngr = new ProductManager();
        //    Ent_Order objodr = Obj;
        //    Orders odr = new Orders();
        //    Products pdr = new Products();

        //   var pr = db.Products.Where(e => e.product_id == Obj.product_id && e.status != "D").SingleOrDefault();

        //    // odr.order_id = objodr.order_id;
        //    odr.user_id = (int)objodr.user_id;
        //    odr.product_id = (int)objodr.product_id;
        //    odr.quantity = objodr.quantity;
        //    odr.total_price = (int)(objodr.quantity * pr.price);
        //    odr.status = "A";
        //    odr.createdBy = "system";
        //    odr.createdDate = DateTime.Now.ToString();
        //    odr.lastModifiedBy = "system";
        //    odr.lastModifiedDate = DateTime.Now.ToString();
        //    return odrmngr.Orderinsert(odr);
        //}

        public HttpResponseMessage Post(Ent_Order ent)
        {

            Orders ord = new Orders();
            ord.user_id = ent.user_id;
            ord.product_id = ent.product_id;
            ord.quantity = ent.quantity;


            int tot = mgr.GetPrice(ord);
            ord.total_price = ent.quantity * tot;


            ord.status = "A";
            ord.createdBy = "Test";
            ord.createdDate = DateTime.Now.ToString();
            ord.lastModifiedBy = "Test1";
            ord.lastModifiedDate = DateTime.Now.ToString();

            string result = mgr.AddOrder(ord);

            if (result == "Error")
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error");
            else
            {

                return Request.CreateResponse(HttpStatusCode.OK, "Added Successfully");

            }
        }





        //api/sample/OrderUpdate (status set to D)
        [HttpPut]
        [Route("orderUpdate")]

        public HttpResponseMessage orderUpdate(int id, [FromBody] Ent_Order Obj)
        {
            OrderManager odrmngr = new OrderManager();
            Ent_Order entobj = Obj;
            Orders odr = new Orders();

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
            Orders ord = new Orders();
            OrderManager odmngr = new OrderManager();
            ord.order_id = id;
            ord.status = "D";
            odmngr.OrderCancelation(ord);

            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }

        //[Route("api/AjaxAPI/SendEmail")]


        [Route("SendMailtoUser")]
        [HttpPost]
        public HttpResponseMessage SendMailtoUser(Ent_Order ent)
        {
            try
            {

                Orders order = new Orders();
                order.user_id = ent.user_id;

                string mailid = mgr.SelectMailid(order);
                mail.Email = mailid;


                mail.Body = "OrderAccepted";
                mail.Subject = "Order Details";


                List<Orders> ord1 = mgr.OrderFullData(order);

                foreach (var obj in ord1)
                {
                    obj.user_id = obj.user_id;
                    obj.status = "D";
                    mgr.UpdateStatus(obj);
                }

                string result = mailMngr.SendEmailId(mail);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [Route("OrderedItems")]
        [HttpPost]
        public HttpResponseMessage OrderFullItems(Ent_Order ent)
        {

            try
            {

                Orders order = new Orders();
                order.user_id = ent.user_id;

                string mailid = mgr.SelectMailid(order);
                mail.Email = mailid;


                List<Orders> ord1 = mgr.OrderFullData(order);
                foreach (var obj in ord1)
                {
                    int tot = mgr.GetPriceByProduct(obj.product_id);
                    order.total_price = obj.quantity * tot;
                    mail.Body += "<h3>Quantity    :   " + obj.quantity + "\nPrice    :    " + tot + "\nTotal   :   " + obj.total_price + "\n</h3>";
                }

                mail.Subject = "Order Details";

                string result = mailMngr.AdminSendMail(mail);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)

            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


    }
}
