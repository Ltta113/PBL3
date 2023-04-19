using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;

namespace PBL3.Controllers
{
    public class DanhSachSanPhamsController : Controller
    {
        private CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: DanhSachSanPhams
        public ActionResult Index(string DanhMucsp)
        {
            if (DanhMucsp == null)
            {
                var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai);
                return View(sanPhams.ToList());
            }
            else
            {
                var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai).Where(p => p.DanhMucSP.TenDanhMuc == DanhMucsp);
                return View(sanPhams.ToList());
            }
        }

        // GET: DanhSachSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
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
