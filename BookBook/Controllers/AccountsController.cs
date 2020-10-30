using BookBook.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;

namespace BookBook.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [OutputCache(Duration =120)]
        public ActionResult Login(User user)
        {
            eCommerceContext db = new eCommerceContext();
            if (ModelState.IsValid)
            {
                var temp = db.Users.FirstOrDefault(m => m.Email == user.Email);

                if (temp != null)
                {
                    if (temp.Password == user.Password)
                    {
                        Session["Account"] = temp.UserId;
                        Session["Email"] = temp.Email;

                        //Cache cache = new Cache();
                        //cache.Add("testCache", "testCache", null, DateTime.MaxValue, TimeSpan.FromDays(1), CacheItemPriority.Normal, null);
                        //cache.Get("testCache");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Error = "User doesn't exist!";
                    return View(user);
                }
            }

            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            eCommerceContext db = new eCommerceContext();
            using (var dbTran = db.Database.BeginTransaction()) 
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var temp = db.Users.FirstOrDefault(m => m.Email == user.Email);

                        if (temp == null)
                        {
                            if (user.Password == user.ConfirmPassword)
                            {
                                user.CreatUser = user.FirstName + " " + user.LastName;
                                user.CreatDate = DateTime.Now;
                                user.EditUser = user.FirstName + " " + user.LastName;
                                user.EditDate = DateTime.Now;
                                user.Status = "Active";

                                db.Users.Add(user);
                                db.SaveChanges();

                                dbTran.Commit();

                                return RedirectToAction("Login");
                            }
                        }
                        else
                        {
                            ViewBag.Error = "User has existed!";
                            dbTran.Rollback();

                            return View(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    ModelState.AddModelError("", "" + ex.Message);

                    return View(user);
                }
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}