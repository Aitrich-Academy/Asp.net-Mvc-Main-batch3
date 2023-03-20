using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;

namespace DAL.Manager
{
    public class OrderManager
    {
        E_COMMERCE1Entities order = new E_COMMERCE1Entities();

        

        //public Order orderDetails(int id)
        //{
        //    Order return_Obj = new Order();
        //   // var list = order.Orders.ToList();
        //    return return_Obj = order.Orders.Where(e => e.order_id == id && e.status != "D").SingleOrDefault();
        //}
        public List<Order> allOrders(int id = 0)
        {
            if (id != 0)
            {
                return order.Orders.Where(e => e.order_id == id && e.status != "D").ToList();
            }
            else
            {
                return order.Orders.Where(e => e.status != "D").ToList();
            }
        }
        public Order orderdetUserId(int id)
        {
            Order return_Obj = new Order();
            return return_Obj = order.Orders.Where(e => e.user_id == id && e.status != "D").SingleOrDefault();
        }

        public List<Order> Product_OrderDetails(int id)
        {
            return order.Orders.Where(p => p.product_id == id).ToList();
        }
        public string AddOrder(Order ord)
        {
            int result = 0;

            order.Orders.Add(ord);
            result = order.SaveChanges();

            if (result > 0)
                return ord.order_id.ToString();
            else
                return "Error ";
        }
        public int GetPrice(Order rd)
        {
            var tot = order.Products.Find(rd.product_id);
            return (int)tot.price;
        }
        public List<Order> allOrdersusr(int id = 0)
        {
            if (id != 0)
            {
                return order.Orders.Where(e => e.user_id == id && e.status != "D").ToList();
            }
            else
            {
                return order.Orders.Where(e => e.status != "D").ToList();
            }
        }
        public string Orderinsert(Order Obj)
        {
            int result = 0;
            var objodr = order.Orders.Where(e => e.order_id == Obj.order_id && e.status != "D").SingleOrDefault();
            if (objodr == null)
            {
                Obj.status = "A";
                order.Orders.Add(Obj);
                result = order.SaveChanges();
            }
            else
            {
                // odr.order_id = objodr.order_id;
                objodr.user_id = (int)Obj.user_id;
                objodr.product_id = (int)Obj.product_id;
                objodr.quantity = Obj.quantity;
                objodr.total_price = Obj.total_price;
                objodr.status = "A";
                objodr.createdBy = "system";
                objodr.createdDate = DateTime.Now.ToString();
                objodr.lastModifiedBy = "system";
                objodr.lastModifiedDate = DateTime.Now.ToString();
                order.Entry(objodr).State = EntityState.Modified;
                result = order.SaveChanges();
            }
            if (result > 0)
            {
                return Obj.order_id.ToString();
            }
            else
            {
                return "Error";
            }
        }
        public void OrderUpdate(Order Obj)
        {
            var objodr = order.Orders.Where(e => e.order_id == Obj.order_id).SingleOrDefault();
            if (objodr != null)
            {
                // odr.order_id = objodr.order_id;
                //objodr.user_id = (int)Obj.user_id;
                //objodr.product_id = (int)Obj.product_id;
                //objodr.quantity = Obj.quantity;
                //objodr.total_price = Obj.total_price;
                objodr.status = "D";
                //objodr.createdBy = "system";
                //objodr.createdDate = DateTime.Now.ToString();
                //objodr.lastModifiedBy = "system";
                //objodr.lastModifiedDate = DateTime.Now.ToString();
                order.Entry(objodr).State = EntityState.Modified;
                order.SaveChanges();
            }

        }

        public List<Order> Ordered_UserDetails(Order uid)
        {
            return order.Orders.Where(p => p.user_id == uid.user_id && p.status != "D").ToList();
        }
        public void OrderCancelation(Order obj)
        {
            var id = order.Orders.SingleOrDefault(e => e.order_id == obj.order_id);


            order.Orders.Remove(id);
            order.SaveChanges();
        }

        public string SelectMailid(Order odr)
        {
            var mailid = order.Users.FirstOrDefault(e => e.user_id == odr.user_id);

            return mailid.email;
        }

        public List<Order> OrderFullData(Order obj)
        {
            return order.Orders.Where(p => p.user_id == obj.user_id).ToList();
        }
        public string UpdateStatus(Order obj)
        {
            var id = order.Orders.FirstOrDefault(p => p.user_id == obj.user_id);
            if (id == null)
                return "Error";
            else
            {
                id.status = obj.status;

                order.Entry(id).State = System.Data.Entity.EntityState.Modified;
                order.SaveChanges();
                return id.order_id.ToString();
            }
        }

        public int GetPriceByProduct(int proid)
        {
            var tot = order.Products.FirstOrDefault(e => e.product_id == proid);
            return (int)tot.price;
        }

    }
}
