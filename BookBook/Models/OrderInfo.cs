using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookBook.Models
{
    public class OrderInfo
    {
        [Key]
        public decimal OrderId { get; set; }
        public decimal Amount { get; set; }
        public string OrderDescription { get; set; }
        public string BankCode { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal vnp_TransactionNo { get; set; }
        public string vpn_Message { get; set; }
        public string vpn_TxnResponseCode { get; set; }
    }
}