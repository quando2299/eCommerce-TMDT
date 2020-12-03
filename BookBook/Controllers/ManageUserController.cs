//using BookBook.Data;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace BookBook.Controllers
//{
//    public class ManageUserController : BaseController
//    {
//        eCommerceContext db = new eCommerceContext();

//        public ActionResult Index()
//        {
//            var list = db.Database.SqlQuery<User>(@"select * from Users u where u.Status = 'Active' and u.UserId > 1").ToList();
//            return View(list);
//        }

//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Create(User user)
//        {
//            using (var dbtran = db.Database.BeginTransaction())
//            {
//                try
//                {
//                    var temp = db.Users.FirstOrDefault(m => m.Email == user.Email);
//                    if (temp == null)
//                    {
//                        if (user.Password == user.ConfirmPassword)
//                        {
//                            user.Status = "Active";
//                            user.CreatUser = "Admin";
//                            user.CreatDate = DateTime.Now;
//                            user.EditUser = "Admin";
//                            user.EditDate = DateTime.Now;

//                            db.Users.Add(user);
//                            db.SaveChanges();
//                            dbtran.Commit();
//                            return RedirectToAction("Index");
//                        }
//                    }
//                    else
//                    {
//                        dbtran.Rollback();
//                        return View(user);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    dbtran.Rollback();
//                    ModelState.AddModelError("", "" + ex.Message);

//                    return View(user);
//                }
//            }
//            return View(user);
//        }

//        public ActionResult Edit(int? id)
//        {
//            var editUser = db.Users.FirstOrDefault(m => m.UserId == id);
//            return View(editUser);
//        }

//        [HttpPost]
//        public ActionResult Edit(User user)
//        {
//            var editUser = db.Users.FirstOrDefault(m => m.UserId == user.UserId);

//            editUser.EditUser = "Admin";
//            editUser.EditDate = DateTime.Now;
//            editUser.FirstName = user.FirstName;
//            editUser.LastName = user.LastName;
//            editUser.Email = user.Email;
//            //editUser.CreatDate = user.CreatDate;
//            //editUser.CreatUser = user.CreatUser;

//            db.Entry(editUser).State = EntityState.Modified;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        //public ActionResult Detail(int? id)
//        //{
//        //    var detailUser = db.Users.FirstOrDefault(m => m.UserId == id);
//        //    return View(detailUser);
//        //}
 
//        public ActionResult Delete(int id)
//        {
//            var deleteRow = db.Users.FirstOrDefault(m => m.UserId == id);
//            deleteRow.Status = "Deactive";
//            deleteRow.EditUser = "Admin";
//            deleteRow.EditDate = DateTime.Now;

//            db.SaveChanges();

//            return RedirectToAction("Index");
//        }
//    }
//}