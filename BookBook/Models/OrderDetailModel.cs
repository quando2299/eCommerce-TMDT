using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class OrderDetailModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int total { get; set; }
    }
}