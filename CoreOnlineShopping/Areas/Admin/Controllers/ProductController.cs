using CoreOnlineShopping.Data;
using CoreOnlineShopping.Models;
using CoreOnlineShopping.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        ProductsRepository _pr = new ProductsRepository();
        SpecialTagRepository _sr = new SpecialTagRepository();
        ProductTypeRepository _ptr = new ProductTypeRepository();
        ApplicationDbContext _db = new ApplicationDbContext();
        [Obsolete]
        private IHostingEnvironment _he;

        [Obsolete]
        public ProductController(IHostingEnvironment c)
        {
            _he = c;
        }

        public IActionResult deneme()
        {

            return View();
        }

        public JsonResult getir()
        {

            

            List<Products> products = new List<Products>();
            
            _db.Products.ToList().ForEach(x => products.Add(new Products
            {
                Id=x.Id,
                Image=x.Image,
                IsAvilable=x.IsAvilable,
                Name=x.Name,
                Price=x.Price,
                ProductColor=x.ProductColor,
                ProductTypesId=x.ProductTypesId,
                SpecialTagId=x.SpecialTagId
            }));
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(products);
            return Json(jsonString);
        } 
        [HttpGet]
        public IActionResult Index()
        {
            return View(_pr.PList());
        }
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            if (lowAmount == null)
            {
                lowAmount = decimal.MinValue;
            }
            if (largeAmount == null)
            {
                largeAmount = Decimal.MaxValue;
            }
            var products =_pr.PList().Where(x => x.Price >= lowAmount && x.Price <= largeAmount).ToList();
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {

            ViewData["productTypeId"] = new SelectList
            (
                _ptr.TList(), "Id", "ProductType"
            );
            ViewData["TagId"] = new SelectList
            (
                _sr.TList(), "Id", "SpeacialTagNAme"
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(Products p, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var search = _db.Products.FirstOrDefault(x => x.Name == p.Name);
                if (search != null)
                {
                    ViewData["productTypeId"] = new SelectList
                       (
                           _ptr.TList(), "Id", "ProductType"
                       );
                    ViewData["TagId"] = new SelectList
                    (
                        _sr.TList(), "Id", "SpeacialTagNAme"
                    );
                    ViewBag.Message = "This Product already exist.";
                    return View(p);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    image.CopyTo(new FileStream(name, FileMode.Create));
                    p.Image = "/Images/" + image.FileName;
                }
                else
                {
                    p.Image = "/Images/NotFound.png";
                }

                _pr.TAdd(p);
                TempData["Save"] = "Product has been saved";
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            TempData["Delete"] = "Product  has been deleted";
            _pr.TDelete(_pr.TGet(id));
            return Json(null);
        }
        public ActionResult Edit(int id)
        {
            ViewData["productTypeId"] = new SelectList
            (
               _ptr.TList(), "Id", "ProductType"
            );
            ViewData["TagId"] = new SelectList
            (
                _sr.TList(), "Id", "SpeacialTagNAme"
            );
            return View(_pr.TGet(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Edit(Products p , IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    image.CopyTo(new FileStream(name, FileMode.Create));
                    p.Image = "/Images/" + image.FileName;
                }
                if (image == null)
                {
                   
                }
                _pr.TUpdate(p);
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(p.Id);
        }
        public ActionResult Details(int id)
        {
            ViewData["productTypeId"] = new SelectList
            (
               _ptr.TList(), "Id", "ProductType"
            );
            ViewData["TagId"] = new SelectList
            (
                _sr.TList(), "Id", "SpeacialTagNAme"
            );
            return View(_pr.TGet(id));
        }
    }
}
