using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class Cart
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string image { get; set; }
        public int total { get; set; }
    }
}