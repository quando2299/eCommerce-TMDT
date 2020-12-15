using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            if (Session["Account"] == null)
                return View("Login", "Accoutnts");

            BookEntity context = new BookEntity();
            var user = context.Database.SqlQuery<user>(
                @"select * from users
                    where id = " + int.Parse(Session["Account"] == null ? "0" : Session["Account"].ToString())).SingleOrDefault();
            return View(user);
        }

        public ActionResult EditProfile(int id = 0)
        {
            BookEntity context = new BookEntity();

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = context.Database.SqlQuery<user>(
                @"select * from users
                    where id = " + int.Parse(Session["Account"] == null ? "0" : Session["Account"].ToString())).SingleOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(user user)
        {
            if (user == null)
                return View("Index");
            

            if (user.password != user.confirm_password)
                return View("Index"); 

            using (BookEntity context = new BookEntity())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try 
                    {
                        var _user = context.Database.SqlQuery<user>(
                            @"select * from users
                                where id = " + int.Parse(Session["Account"] == null ? "0" : Session["Account"].ToString())).SingleOrDefault();

                        _user.email = (user.email != null) ? user.email : _user.email;
                        _user.phone = (user.phone != null) ? user.phone : _user.phone;
                        _user.firstname = (user.firstname != null) ? user.firstname : _user.firstname;
                        _user.lastname = (user.lastname != null) ? user.lastname : _user.lastname;
                        _user.alteruser = _user.firstname + " " + _user.lastname;
                        _user.alterdate = DateTime.Now;
                        _user.password = (user.password != null) ? user.password : _user.password;
                        _user.confirm_password = (user.confirm_password != null) ? user.confirm_password : _user.confirm_password;
                        _user.address = (user.address != null) ? user.address : _user.address;

                        context.Entry(_user).State = EntityState.Modified;
                        context.SaveChanges();

                        transaction.Commit();

                        return RedirectToAction("Index", "Personal");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        ViewBag.ErrorText = e.Message.ToString();

                        return View(user);
                    }
                }
            }
        }

        public ActionResult Order()
        {
            BookEntity context = new BookEntity();
            return View(context.orders.ToList().Where(model => model.userid == int.Parse(Session["Account"].ToString())));
        }

        public ActionResult Bill()
        {
            BookEntity context = new BookEntity();
            return View(context.bills.ToList().Where(model => model.userid == int.Parse(Session["Account"].ToString())));
        }
    }
}