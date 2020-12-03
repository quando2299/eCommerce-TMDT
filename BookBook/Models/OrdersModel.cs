using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class OrdersModel
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int total { get; set; }
        public DateTime createdate { get; set; }
        public int status { get; set; }
        public string fullname { get; set; }
    }
}