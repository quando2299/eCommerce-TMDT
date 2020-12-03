//using BookBook.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace BookBook.Controllers
//{
//    public class ManagerController : BaseController
//    {
//        // GET: Manager
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult Done(Bill bill)
//        {
//            eCommerceContext db = new eCommerceContext();
//            var temp = db.Bills.FirstOrDefault(m => m.ID == bill.ID);


//            temp.UpdateUser = "Admin";
//            temp.UpdateDate = DateTime.Now;
//            temp.Status = 3;

//            db.Entry(temp).State = EntityState.Modified;
//            db.SaveChanges();
//            return RedirectToAction("ManageBill", "Cart");
//        }

//        public ActionResult Delete(int id)
//        {
//            eCommerceContext db = new eCommerceContext();
//            var list = db.BillDetails.Where(m => m.BillID == id).ToList();
//            foreach (var item in list)
//            {
//                item.Status = 99;
//                db.Entry(item).State = EntityState.Modified;
//            }

//            var bill = db.Bills.FirstOrDefault(m => m.ID == id);
//            bill.Status = 99;

//            db.Entry(bill).State = EntityState.Modified;
//            db.SaveChanges();
//            return RedirectToAction("ManageBill", "Cart");
//        }

//        public ActionResult Detail(int id)
//        {
//            eCommerceContext db = new eCommerceContext();
//            var list = db.Database.SqlQuery<BillDetailView>(@"
//                select bd.Quantity, p.ProductName
//                from BillDetail bd
//                    inner join Products p on p.ProductId = bd.ProductID
//                    inner join Bill b on b.ID = bd.BillID
//                    inner join Users u on u.UserId = b.UserID
//                where b.ID = {0}
//            ", id).ToList();
//            return View(list);
//        }
//    }
//}