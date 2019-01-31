using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        ShopContext db = new ShopContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _GetCategories()
        {
            var Categories = db.Categories.ToList();

            return PartialView(Categories);
        }

        public ActionResult GetProducts(int categoryId)
        {
            var Products = db.Products.Where(x => x.CategoryId == categoryId).ToList();

            return View(Products);
        }

        public ActionResult _GetRandomDiscountedProduct()
        {
            var products = db.Products.Where(p => p.Discount == true).ToList();

            int selectedIndex = (int)(DateTime.Now.Ticks % products.Count);

            var product = products[selectedIndex];

            return PartialView(product);
        }

        public ActionResult Details(int Id)
        {
            var product = db.Products.Find(Id);
            return View(product);
        }

        public ActionResult _GetAdminMenu()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}