using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    public class HoaDonsController : LoginManagerController
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/HoaDons
        public ActionResult Index(string Searchtxt, string currentFilter, int? page)
        {
            if (Searchtxt != null)
            {
                page = 1;
            }
            else
            {
                Searchtxt = currentFilter;
            }

            ViewBag.CurrentFilter = Searchtxt;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (Searchtxt == null)
            {
                var hoaDons = db.HoaDons.Include(h => h.User).OrderBy(h => h.Status).OrderBy(h => h.NgayBan).ToList();
                return View(hoaDons.ToPagedList((int)pageNumber, (int)pageSize));
            }
            else
            {
                var hoaDons = db.HoaDons.Include(h => h.User).Where(h =>h.User.Ten.Contains(Searchtxt))
                    .OrderBy(h => h.Status).OrderBy(h => h.NgayBan).ToList();
                return View(hoaDons.ToPagedList((int)pageNumber, (int)pageSize));
            }

        }
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null || hoaDon.Status == 2)
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
