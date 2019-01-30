using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Item
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}