using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class ProductsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string author { get; set; }
        public string publishing_company { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string typename { get; set; }
    }
}