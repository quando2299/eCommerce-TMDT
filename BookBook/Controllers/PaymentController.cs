using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace BookBook.Controllers
{
    public class PaymentController : Controller
    {
        private static readonly ILog log =
          LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Payment
        public ActionResult Index()
        {

            return View();
        }
    }
}