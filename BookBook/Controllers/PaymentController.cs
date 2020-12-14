using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using BookBook.Models;
using BookBook.Models.Utils;
using BookBook.Database;

namespace BookBook.Controllers
{
    public class PaymentController : Controller
    {
        private static readonly ILog log =
          LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Payment
        public ActionResult Index()
        {

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.Amount = 10000;
            orderInfo.OrderDescription = "Noi dung thanh toan:" + DateTime.Now.ToString("yyyyMMddHHmmss");
            return View(orderInfo);
        }

        [HttpPost]
        public ActionResult Index(OrderInfo orderInfo)
        {
            //Get Config Info
            string vnp_Returnurl = "http://quando2299-001-site1.dtempurl.com/Payment/Complete"; //URL nhan ket qua tra ve 
            string vnp_Url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "OYI7MIDT"; //Ma website
            string vnp_HashSecret = "VJARUOFILYZXWEPPYBOQAXTFGHKDQOAS"; //Chuoi bi mat

            //Get payment input
            OrderInfo order = new OrderInfo();
            //Save order to db
            order.OrderId = DateTime.Now.Ticks;
            order.Amount = Convert.ToDecimal(orderInfo.Amount);
            order.OrderDescription = orderInfo.OrderDescription;
            order.CreatedDate = DateTime.Now;

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.0.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());
            vnpay.AddRequestData("vnp_OrderInfo", order.OrderDescription);
            vnpay.AddRequestData("vnp_OrderType", 150000.ToString()); //default value: other
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_BankCode", "NCB");

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            log.InfoFormat("VNPAY URL: {0}", paymentUrl);

            return Redirect(paymentUrl);
        }

        public ActionResult Complete()
        {
            return View();
        }
    }
}