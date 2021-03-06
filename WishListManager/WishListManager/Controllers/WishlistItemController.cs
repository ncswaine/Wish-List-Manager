﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WishListManager.Models;

namespace WishListManager.Controllers
{
    public class WishlistItemController : Controller
    {
        private WishlistManagerEntities db = new WishlistManagerEntities();

        // GET: wishlist_item
        public ActionResult Index()
        {

            var wishlist_item = from m in db.wishlist_item
                                where m.is_deleted == false
                                orderby m.person.name
                                select m;
            db.wishlist_item.Include(w => w.person);
            ViewBag.deleteSuccess = TempData["deleteSuccess"];

            return View(wishlist_item.ToList());
        }

        // GET: wishlist_item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist_item wishlist_item = db.wishlist_item.Find(id);
            ViewBag.addSuccess = TempData["addSuccess"];
            ViewBag.editSuccess = TempData["editSuccess"];
            if (wishlist_item == null)
            {
                return HttpNotFound();
            }
            return View(wishlist_item);
        }

        // GET: wishlist_item/Add
        public ActionResult Add()
        {
            ViewBag.person_id = new SelectList(db.people.OrderBy(x => x.name), "id", "name");
            return View();
        }

        // POST: wishlist_item/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "id,person_id,description,type,is_purchased,is_deleted")] wishlist_item wishlist_item)
        {
            if (ModelState.IsValid)
            {
                
                db.wishlist_item.Add(wishlist_item);
                db.SaveChanges();
                TempData["addSuccess"] = true;
                return RedirectToAction("Details",new {id = wishlist_item.id});
            }

            ViewBag.person_id = new SelectList(db.people.OrderBy(x => x.name), "id", "name", wishlist_item.person_id);
            return View(wishlist_item);
        }

        // GET: wishlist_item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist_item wishlist_item = db.wishlist_item.Find(id);
            if (wishlist_item == null)
            {
                return HttpNotFound();
            }
            ViewBag.person_id = new SelectList(db.people.OrderBy(x => x.name), "id", "name", wishlist_item.person_id);
             
            return View(wishlist_item);
        }

        // POST: wishlist_item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,person_id,description,type,is_purchased,is_deleted")] wishlist_item wishlist_item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishlist_item).State = EntityState.Modified;
                db.SaveChanges();
                TempData["editSuccess"] = true;
                return RedirectToAction("Details", new { id = wishlist_item.id });
            }
            ViewBag.person_id = new SelectList(db.people.OrderBy(x => x.name), "id", "name", wishlist_item.person_id);
            return View(wishlist_item);
        }

        // GET: wishlist_item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist_item wishlist_item = db.wishlist_item.Find(id);
            if (wishlist_item == null)
            {
                return HttpNotFound();
            }
            return View(wishlist_item);
        }

        // POST: wishlist_item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            wishlist_item wishlist_item = db.wishlist_item.Find(id);
            //db.wishlist_item.Remove(wishlist_item);
            wishlist_item.is_deleted = true;
            db.SaveChanges();
            TempData["deleteSuccess"] = true;
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
