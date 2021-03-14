using CoreOnlineShopping.Models;
using CoreOnlineShopping.Repository;
using CoreOnlineShopping.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {

        OrderRepository _or = new OrderRepository();
        
        
        [HttpGet]
        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CheckOut(Order order)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails det = new OrderDetails();
                    det.ProductId = product.Id;
                    order.OrderDetails = new List<OrderDetails>();
                    order.OrderDetails.Add(det);
                }
            }
            order.OrderNo = GetOrderNumber();
            _or.TAdd(order);
            HttpContext.Session.Set("products", new List<Products>());
            return View("SiparisAlindi",order.OrderNo);
        }

        public IActionResult SiparisAlindi(string orderNo)
        {
            return View(orderNo);
        }

       public string GetOrderNumber()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D" };
            int k1, k2, k3, s1, s2, s3;
            k1 = rnd.Next(0, 4);
            k2 = rnd.Next(0, 4);
            k3 = rnd.Next(0, 4);
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(100, 1000);
            s3 = rnd.Next(100, 1000);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            return kod;
        }

    }
}
