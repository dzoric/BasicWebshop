﻿using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        ShopContext db = new ShopContext();
        public ActionResult Index()
        {
            var lastCategory = db.Categories.ToList().OrderBy(x => x.Id).LastOrDefault().Id;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int categoryId = rnd.Next(1, lastCategory);
            return View(categoryId);
        }

        public ActionResult _GetRandomNewProduct(int categoryId)
        {
            var products = db.Products.Where(p => p.NewProduct == true && p.CategoryId == categoryId).ToList();
            int selectedIndex = (int)(DateTime.Now.Ticks % products.Count);
            var product = products[selectedIndex];
            return PartialView(product);
        }

        public ActionResult _GetCategories()
        {
            var Categories = db.Categories.ToList();
            return PartialView(Categories);
        }

        public ActionResult GetProducts(int categoryId, int? page, string orderBy)
        {
            var Products = db.Products.Where(x => x.CategoryId == categoryId).AsQueryable();
            switch (orderBy)
            {
                case "NameDesc":
                    Products = Products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    Products = Products.OrderBy(p => p.Discount == true ? p.DiscountPrice : p.Price);
                    break;
                case "PriceDesc":
                    Products = Products.OrderByDescending(p => p.Discount == true ? p.DiscountPrice : p.Price);
                    break;
                default:
                    Products = Products.OrderBy(p => p.Name);
                    break;
            }
            return View(Products.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult SearchAllProducts(int? page, string search, string orderBy)
        {
            var Products = db.Products.AsQueryable();
            if (string.IsNullOrEmpty(search) == false)
            {
                Products = Products.Where(p => p.Name.ToLower().Contains(search.ToLower()) || p.Description.ToLower().Contains(search.ToLower()));
            }

            switch (orderBy)
            {
                case "NameDesc":
                    Products = Products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    Products = Products.OrderBy(p => p.Discount == true ? p.DiscountPrice : p.Price);
                    break;
                case "PriceDesc":
                    Products = Products.OrderByDescending(p => p.Discount == true ? p.DiscountPrice : p.Price);
                    break;
                default:
                    Products = Products.OrderBy(p => p.Name);
                    break;
            }

            return View(Products.ToList().ToPagedList(page ?? 1, 3));
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

        public ActionResult _Search()
        {
            return PartialView();
        }

        public ActionResult _GetCartCount()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Stranica koja opisuje vašu aplikaciju.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Vaša kontakt stranica.";

            return View();
        }
    }
}