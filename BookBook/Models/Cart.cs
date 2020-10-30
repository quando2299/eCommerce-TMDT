using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int Total { get; set; }
    }
}