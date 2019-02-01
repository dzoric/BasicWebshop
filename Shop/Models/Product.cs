using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Naziv proizvoda")]
        public string Name { get; set; }
        [Display(Name = "Opis proizvoda")]
        public string Description { get; set; }
        [Display(Name = "Cijena")]
        public decimal Price { get; set; }
        [Display(Name = "Popust")]
        public bool Discount { get; set; }
        [Display(Name = "Cijena s popustom")]
        public decimal DiscountPrice { get; set; }
        [Display(Name = "Slika proizvoda")]
        public string ImageUrl { get; set; }
        [Display(Name = "Jamstvo")]
        public int Warranty { get; set; }
        [Display(Name = "Dostupan")]
        public bool Available { get; set; }
        [Display(Name = "Novi proizvod")]
        public bool NewProduct { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}