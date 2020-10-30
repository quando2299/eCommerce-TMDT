using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class BillView
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public int Total { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public int Status { get; set; }
    }
}