using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using BookBook.Database;
using System.Data.Entity.Validation;
using System.Diagnostics;

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
        public ActionResult Login(user user)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();
            if (ModelState.IsValid)
            {
                var temp = context.users.FirstOrDefault(m => m.email == user.email);

                if (temp != null)
                {
                    if (temp.password == user.password)
                    {
                        Session["Account"] = temp.id;
                        Session["Email"] = temp.email;

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
        public ActionResult Register(user _user)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();

            using (var dbTran = context.Database.BeginTransaction()) 
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var temp = context.users.FirstOrDefault(m => m.email == _user.email);

                        if (temp == null)
                        {
                            if (_user.password == _user.confirm_password)
                            {
                                _user.createuser = _user.firstname + " " + _user.lastname;
                                _user.createdate = DateTime.Now;
                                _user.alteruser = _user.firstname + " " + _user.lastname;
                                _user.alterdate = DateTime.Now;
                                _user.status = 1;
                                _user.isadmin = 0;

                                context.users.Add(_user);
                                context.SaveChanges();

                                dbTran.Commit();

                                return RedirectToAction("Login");
                            }
                        }
                        else
                        {
                            ViewBag.Error = "User has existed!";
                            dbTran.Rollback();

                            return View(_user);
                        }
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }

                    dbTran.Rollback();

                    return View(_user);
                }
            }

            return View(_user);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}