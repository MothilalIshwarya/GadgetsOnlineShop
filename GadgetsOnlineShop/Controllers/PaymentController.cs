using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using GadgetsOnlineShop.Models;

namespace GadgetsOnlineShop.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        GadgetDetailEntities2 gadgetDetailEntities = new GadgetDetailEntities2();
        public ActionResult Index()

        {
            List<Payment> plist = gadgetDetailEntities.Payments.ToList();
            List<Payment> cd = gadgetDetailEntities.Payments.Where(x => x.UserName == User.Identity.Name).ToList();

            return View(cd);
            
        }
        public ActionResult Create(CartDetail cartDetail)
        {
            List<CartDetail> plist = gadgetDetailEntities.CartDetails.Where(x=>x.CartId==cartDetail.CartId && x.UserName == User.Identity.Name).ToList();


            return View(plist);
        }
        //[HttpPost]
        public ActionResult PayDetail(CartDetail cartDetail)
        {
            List<Payment> li = new List<Payment>();
            CartDetail p = (CartDetail)gadgetDetailEntities.CartDetails.Where(x => x.CartId == cartDetail.CartId && x.UserName == User.Identity.Name).SingleOrDefault();
            Payment c = new Payment();
            c.CartId = p.CartId;
            c.CartProductName = p.CartProductName;
            c.CartImage = p.CartImage;
            c.Price = p.CartPrice;
            c.UserName = User.Identity.Name;
            c.OrderDate = DateTime.Now.ToString("dd/MM/yyyy");
            c.DeliveryDate = DateTime.Now.AddDays(14).ToString("dd/MM/yyyy");
            

            if (Session["Payment"] == null)
            {
                li.Add(c);
                Session["Payment"] = li;
                gadgetDetailEntities.Payments.Add(c);
                gadgetDetailEntities.SaveChanges();

            }
            
           else
            {
                
                gadgetDetailEntities.Payments.Add(c);
                gadgetDetailEntities.SaveChanges();
           }
           
            return RedirectToAction("DeliveryInfo");
        }
        public ActionResult DeliveryInfo()
        {
            var model = (from t in gadgetDetailEntities.DeliveryDetails
                         where t.UserName == User.Identity.Name
                         select t.DeliveryId).ToArray();

            List<DeliveryDetail> productdetaillist = gadgetDetailEntities.DeliveryDetails.ToList();
            List<DeliveryDetail> cd = gadgetDetailEntities.DeliveryDetails.Where(x => x.UserName == User.Identity.Name).ToList();
           
            if (model.Length == 0)
            {
                return RedirectToAction("Create", "DeliveryDetails");
            }
            else
            {
                
                    return RedirectToAction("Edit","DeliveryDetails");
               
            }
            

        }

        public ActionResult Edit(int id)
        {
            
            DeliveryDetail deliveryDetail = gadgetDetailEntities.DeliveryDetails.Find(id);
            if (deliveryDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(gadgetDetailEntities.UserDetails, "UserName", "UserPassword", deliveryDetail.UserName);
            return View(deliveryDetail);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryId,UserName,DeliveryDistrict,DeliveryState,UserContactNo,DeliveryAddress")] DeliveryDetail deliveryDetail)
        {
            if (ModelState.IsValid)
            {
                gadgetDetailEntities.Entry(deliveryDetail).State = EntityState.Modified;
                gadgetDetailEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(gadgetDetailEntities.UserDetails, "UserName", "UserPassword", deliveryDetail.UserName);
            return View(deliveryDetail);
        }
       
    }
}