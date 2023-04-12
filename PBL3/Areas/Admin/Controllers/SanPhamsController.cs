using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;
using System.IO;
using System.Web.Mvc;

namespace PBL3.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai);
            return View(sanPhams.ToList());
        }

        // GET: Admin/SanPhams/Details/5
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

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc");
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SP,ID_Danhmuc,TenSP,ID_KM,SoLuong,GiaBan,NhanHieuSP,MauSP,MoTaSP,Status")] SanPham sanPham, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if(uploadhinh != null || uploadhinh.ContentLength >0)
                {
                    string hinh = uploadhinh.FileName.ToString();
                    sanPham.Anh = hinh;
                }
                sanPham.Status = 1;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                    
                return RedirectToAction("Index");
            }

            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc", sanPham.ID_Danhmuc);
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM", sanPham.ID_KM);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc", sanPham.ID_Danhmuc);
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM", sanPham.ID_KM);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SP,ID_Danhmuc,TenSP,ID_KM,SoLuong,GiaBan,NhanHieuSP,MauSP,MoTaSP,Anh,Status")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc", sanPham.ID_Danhmuc);
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM", sanPham.ID_KM);
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
