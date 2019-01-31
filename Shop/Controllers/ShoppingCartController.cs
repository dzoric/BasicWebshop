using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShopContext db = new ShopContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View("Cart");
        }

        public ActionResult AddToCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            if(cart==null)
            {
                cart = new List<Item>();
                var product = db.Products.Find(productId);
                Item item = new Item();
                item.Product = product;
                item.Count = 1;
                cart.Add(item);
                Session["cart"] = cart;
            }
            else
            {
                int location = cart.FindIndex(x => x.Product.Id == productId);
                //if element is not found, FindIndex returns -1
                if (location == -1)
                {
                    var product = db.Products.Find(productId);
                    Item item = new Item();
                    item.Product = product;
                    item.Count = 1;
                    cart.Add(item);
                }
                else //element is found on location saved in variable location
                {
                    cart[location].Count++;
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == productId);
            cart.RemoveAt(location);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult IncreaseCount(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == productId);
            cart[location].Count++;
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult DecreaseCount(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == productId);
            if (cart[location].Count==1)
            {
                cart.RemoveAt(location);
            }
            else
            {
                cart[location].Count--;
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(AddressInformation information)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.Timeout = 10000;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("tecajskimail@gmail.com", "T3cajtecaj");

            string mailMessage = "Nova narudžba je zaprimljena!" + Environment.NewLine +
                " Kupac:" + information.FirstName + " " + information.LastName + Environment.NewLine
                + "Adresa: " + Environment.NewLine + information.City + Environment.NewLine + information.Address
                + Environment.NewLine + information.ZipCode + Environment.NewLine + information.PhoneNumber + Environment.NewLine;
            var products = (List<Item>)Session["cart"];
            decimal total = 0;
            foreach(var product in products)
            {
                if (product.Product.Discount == true)
                {
                    total += (product.Count * product.Product.DiscountPrice);
                }
                else
                {
                    total += (product.Count * product.Product.Price);
                }

                mailMessage += (product.Product.Name + " - " + product.Count + " komada") + Environment.NewLine;
            }
            mailMessage += "Ukupni iznos narudžbe: " + total;

            MailMessage message = new MailMessage("tecajskimail@gmail.com", "dusan8@ymail.com", "Narudžba", mailMessage);

            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(message);
            return View("OrderCompleted");
        }
    }
}