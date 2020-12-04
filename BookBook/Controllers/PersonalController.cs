using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookBook.Database;

namespace BookBook.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(user user)
        {
            if (user == null)
            {
                return View("Index");
            }
            using (BookEntity context = new BookEntity())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        return View("Index", "Personal");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        ViewBag.ErrorText = e.Message.ToString();

                        return View();
                    }
                }
            }
        }
    }
}