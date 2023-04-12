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
    public class HoaDonsController : Controller
    {
        private CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.User);
            return View(hoaDons.ToList());
        }
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return RedirectToAction("Index");
            }
            hoaDon.Status = 1;
            db.Entry(hoaDon).State = EntityState.Modified;
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
