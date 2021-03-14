using CoreOnlineShopping.Data;
using CoreOnlineShopping.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        ProductTypeRepository _c = new ProductTypeRepository();
         
        public IActionResult Index()
        {
     
            return View(_c.TList());
        }
        

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ProductTypes p)
        {
            if (ModelState.IsValid)
            {
                _c.TAdd(p);
                TempData["Save"] = "Product Type has been saved";
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            TempData["Delete"] = "Product Type has been deleted";
            _c.TDelete(_c.TGet(id));
            return Json(null);
        }
        public ActionResult Edit(int id)
        {
            return View(_c.TGet(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.ProductTypes p)
        {
            if (ModelState.IsValid)
            {
                _c.TUpdate(p);
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(p.Id);
        }
        public ActionResult Details(int id)
        {  
            return View(_c.TGet(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Models.ProductTypes p)
        {
            return RedirectToAction("Index");
        }
    }
}
