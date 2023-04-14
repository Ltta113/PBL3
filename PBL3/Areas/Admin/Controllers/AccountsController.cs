using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    public class AccountsController : LoginManagerController
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/Accounts
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.User);
            return View(accounts.ToList());
        }


        // GET: Admin/Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Account = new SelectList(db.Users, "ID_User", "Ten", account.ID_Account);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Account,Username,Password,Quyen")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Account = new SelectList(db.Users, "ID_User", "Ten", account.ID_Account);
            return View(account);
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
