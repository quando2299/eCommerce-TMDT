using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class CheckOutView
    {
        [DisplayName("Họ Tên")]
        public string FullName { get; set; }
        public int Total { get; set; }
        public int DiscountID { get; set; }
        public int UserID { get; set; }
    }
}       