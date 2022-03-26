using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetsOnlineShop.Models;

namespace GadgetsOnlineShop.Controllers
{
    public class CartController : Controller
    {
        GadgetDetailEntities2 gadgetdetailentities = new GadgetDetailEntities2();
        ProductDetail productDetail = new ProductDetail();
        CartDetail cartDetail = new CartDetail();
        // GET: Cart
        public ActionResult Index()
        {
           
            return View();
        }

        List<CartDetail> li = new List<CartDetail>();
        
        //[HttpPost]
        [Authorize]
        public ActionResult CartData(int productid)
        {
           
            List<CartDetail> li = new List<CartDetail>();
            //bool product = IsValid(productid);// gadgetdetailentities.CartDetails.Find(productid);
            ProductDetail p = (ProductDetail)gadgetdetailentities.ProductDetails.Where(x => x.ProductId == productid).SingleOrDefault();
            CartDetail c = new CartDetail();
            c.CartId = productid;
            c.CartProductName = p.ProductName;
            c.CartImage = p.ProductImage;
            c.CartPrice = p.ProductPrice;
            c.UserName = User.Identity.Name;
            c.CartNo = c.CartNo;
           
            bool pro=true;           
            var model = (from t in gadgetdetailentities.CartDetails where t.UserName == User.Identity.Name && t.CartId == productid
                         select t.CartId).ToArray();
            if (model.Length == 0)
            {
                 pro = true;
            }
            else
            {
                 pro = false;
            }
            
            if (Session["CartDetail"] == null )
            {
                li.Add(c);
                Session["CartDetail"] = li;
                if (ModelState.IsValid && pro)
                {
                    gadgetdetailentities.CartDetails.Add(c);
                    gadgetdetailentities.SaveChanges();
                }
            }
            else { 

                if (ModelState.IsValid && pro)
                {
                 gadgetdetailentities.CartDetails.Add(c);
                 gadgetdetailentities.SaveChanges();
                }
            }
            return RedirectToAction("CartList");
        }
        private bool IsValid(int id)
        {
            var model = from t in gadgetdetailentities.CartDetails where t.UserName == User.Identity.Name & t.CartId==id select t;
                      
                if (model==null)
                {
                    return false;
                }
            
            return true;
        }

        [Authorize]
        public ActionResult CartList()
        {
            List<CartDetail> productdetaillist = gadgetdetailentities.CartDetails.ToList();
            List<CartDetail> cd = gadgetdetailentities.CartDetails.Where(x => x.UserName == User.Identity.Name).ToList();

            return View(cd);
            
        }
        public ActionResult RemoveList(int cartno)
        {
            CartDetail cartDetail = gadgetdetailentities.CartDetails.Find(cartno);
            gadgetdetailentities.CartDetails.Remove(cartDetail);
            gadgetdetailentities.SaveChanges();
            return RedirectToAction("CartList");
        }
             
        
       
    }
    
}
