using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GadgetsOnlineShop.Models;

namespace GadgetsOnlineShop.Controllers
{

    public class HomeController : Controller
    {
        GadgetDetailEntities2 gadgetdetailentities = new GadgetDetailEntities2();
        ProductDetail productdetail = new ProductDetail();
        public ActionResult Index()
        {
            List<ProductDetail> productdetaillist = gadgetdetailentities.ProductDetails.ToList();
            
            return View(productdetaillist);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Category(String categoryName)
        {
            List<ProductDetail> pd = new List<ProductDetail>();
            var model = gadgetdetailentities.ProductDetails.Where(x => x.CategoryName == categoryName).ToList();

            return View(model);
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(UserDetail userdetail)
        {
           
            var model = (from t in gadgetdetailentities.UserDetails
                         where t.UserName == userdetail.UserName
                         select t.UserId).ToArray();
           
            if (ModelState.IsValid && model.Length==0)
            {
                GadgetDetailEntities2 db = new GadgetDetailEntities2();
                db.UserDetails.Add(userdetail);
                db.SaveChanges();
                return RedirectToAction("Signin");
            }
            
            return View(userdetail);
            
        }
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(UserDetail model)
        {
            
            if (ModelState.IsValid)
            {
                
                using (GadgetDetailEntities2 db = new GadgetDetailEntities2())
                {
                    var obj = db.UserDetails.Where(a => a.UserName.Equals(model.UserName) || a.UserPassword.Equals(model.UserPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index");

                    }

                }
            }
            return View(model);
        }
      
        private bool Isvalid(UserDetail model)
        {
            int countno = 0;
            List<UserDetail> detail = gadgetdetailentities.UserDetails.ToList();
            foreach (var m in detail)
            {
                if (m.UserName == model.UserName && m.UserPassword == model.UserPassword)
                {
                    countno = 1;
                    break;
                }
            }
            if (countno == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult Search(String search)
        {
            if (search == "ProductName")
            {

                var model = gadgetdetailentities.ProductDetails.Where(x => x.ProductName == search).ToList();
                return View(model);
            }
            else
            {
                var model = gadgetdetailentities.ProductDetails.Where(x => x.ProductName == search).ToList();
                return View(model);
            }
            
        }
        public ActionResult MoreDetail(String productname)
        {
            if (productname == "ProductId")
            {
                var model = gadgetdetailentities.ProductDetails.Where(x => x.ProductName == productname).ToList();
                return View(model);
            }
            else
            {
                var model = gadgetdetailentities.ProductDetails.Where(x => x.ProductName == productname).ToList();
                return View(model);
            }
        }
    }
}