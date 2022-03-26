using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GadgetsOnlineShop.Models;

namespace GadgetsOnlineShop.Controllers
{
    public class DeliveryDetailsController : Controller
    {
        private GadgetDetailEntities2 db = new GadgetDetailEntities2();

        // GET: DeliveryDetails
        public ActionResult Index()
        {
            var deliveryDetails = db.DeliveryDetails.Include(d => d.UserDetail);
            return View(deliveryDetails.ToList());
        }

        // GET: DeliveryDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryDetail deliveryDetail = db.DeliveryDetails.Find(id);
            if (deliveryDetail == null)
            {
                return HttpNotFound();
            }
            return View(deliveryDetail);
        }

        // GET: DeliveryDetails/Create
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "UserPassword");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeliveryId,UserName,DeliveryDistrict,DeliveryState,UserContactNo,DeliveryAddress")] DeliveryDetail deliveryDetail)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryDetails.Add(deliveryDetail);
                db.SaveChanges();
                return RedirectToAction("Index","Payment");
            }

            ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "UserPassword", deliveryDetail.UserName);
            return View(deliveryDetail);
        }

        // GET: DeliveryDetails/Edit/5
        public ActionResult Edit()
        {
            DeliveryDetail d = new DeliveryDetail();
            DeliveryDetail deliveryDetail = (DeliveryDetail)db.DeliveryDetails.Where(x=>x.UserName==User.Identity.Name).SingleOrDefault();
            d.DeliveryId = deliveryDetail.DeliveryId;
            d.DeliveryAddress = deliveryDetail.DeliveryAddress;
            d.DeliveryDistrict = deliveryDetail.DeliveryDistrict;
            d.DeliveryState = deliveryDetail.DeliveryState;
            d.UserContactNo = deliveryDetail.UserContactNo;
            d.UserName = User.Identity.Name;
            if (d == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "UserPassword", deliveryDetail.UserName);
            return View(d);
        }

        // POST: DeliveryDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryId,UserName,DeliveryDistrict,DeliveryState,UserContactNo,DeliveryAddress")] DeliveryDetail deliveryDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Payment");
            }
            ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "UserPassword", deliveryDetail.UserName);
            return View(deliveryDetail);
        }
       
        // GET: DeliveryDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryDetail deliveryDetail = db.DeliveryDetails.Find(id);
            if (deliveryDetail == null)
            {
                return HttpNotFound();
            }
            return View(deliveryDetail);
        }

        // POST: DeliveryDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryDetail deliveryDetail = db.DeliveryDetails.Find(id);
            db.DeliveryDetails.Remove(deliveryDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
