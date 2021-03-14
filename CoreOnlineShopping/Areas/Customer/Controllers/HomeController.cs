using CoreOnlineShopping.Data;
using CoreOnlineShopping.Models;
using CoreOnlineShopping.Repository;
using CoreOnlineShopping.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Mvc.Core.Common;

namespace CoreOnlineShopping.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        ProductsRepository _pr = new ProductsRepository();
        ProductTypeRepository _ptr = new ProductTypeRepository();
        SpecialTagRepository _sr = new SpecialTagRepository();

        private readonly ILogger<HomeController> _logger;
        //public HomeController(ProductsRepository pr)
        //{
        //    _pr = pr;
        //}


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int page=1)
        {
            
            return View(_pr.PList().ToPagedList(page,9));
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(_pr.TGet(Convert.ToInt32(id)));
        }
        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            products.Add(_pr.TGet(Convert.ToInt32(id)));
            HttpContext.Session.Set("products", products);
            return View(_pr.TGet(Convert.ToInt32(id)));
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return View("Details", _pr.TGet(Convert.ToInt32(id)));
        }
        [HttpGet]
        [ActionName("Remove")]
        public ActionResult CardRemove(int id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction("Card");
        }
        public ActionResult Card()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}
