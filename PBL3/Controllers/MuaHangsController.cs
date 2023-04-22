﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;
using PBL3.Models.Model_View;

namespace PBL3.Controllers
{
    public class MuaHangsController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        // GET: MuaHangs
        public ActionResult MuaNgay(FormCollection form)
        {
            if (Session["ID_Account"].Equals("") == false)
            {
                HoaDon hoadon = new HoaDon();
                hoadon.ID_User = Convert.ToInt32(Session["ID_Account"]);
                hoadon.NgayBan = DateTime.Now;
                hoadon.Status = 0;
                db.HoaDons.Add(hoadon);
                ChiTietHoaDon chitiethoadon = new ChiTietHoaDon();
                chitiethoadon.ID_HoaDon = hoadon.ID_HoaDon;
                int id = Convert.ToInt32(form["idSanPham"]);
                int soluong = Convert.ToInt32(form["SoLuongSanPham"]);
                chitiethoadon.ID_SP = id;
                chitiethoadon.SoLuong = soluong;
                db.ChiTietHoaDons.Add(chitiethoadon);
                var sanpham = db.SanPhams.FirstOrDefault(s => s.ID_SP == id);
                sanpham.SoLuong -= soluong;
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThanhCong", "DatMua");
            }
            else
            {
                Response.Redirect("~/dang-nhap");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DatMua()
        {

            GioHangs giohang = Session["Giohang"] as GioHangs;
            if (Session["ID_Account"].Equals("") == false)
            {
                HoaDon hoadon = new HoaDon();
                hoadon.ID_User = Convert.ToInt32(Session["ID_Account"]);
                hoadon.NgayBan = DateTime.Now;
                hoadon.Status = 0;
                db.HoaDons.Add(hoadon);
                foreach (var item in giohang.Items)
                {
                    if (item.Status == true || item.SoLuong != 0)
                    {
                        ChiTietHoaDon chitiethoadon = new ChiTietHoaDon();
                        chitiethoadon.ID_HoaDon = hoadon.ID_HoaDon;
                        int id = item.SanPhamGioHang.ID_SP;
                        int soluong = item.SoLuong;
                        chitiethoadon.ID_SP = id;
                        chitiethoadon.SoLuong = soluong;
                        db.ChiTietHoaDons.Add(chitiethoadon);
                        var sanpham = db.SanPhams.FirstOrDefault(s => s.ID_SP == id);
                        sanpham.SoLuong -= soluong;
                        db.Entry(sanpham).State = EntityState.Modified;
                        GioHang gh = db.GioHangs.FirstOrDefault(s => s.ID_SP == id);
                        if (gh != null)
                            db.GioHangs.Remove(gh);
                    }
                }
                giohang.XoaSanPhamDaMua();
                db.SaveChanges();
                return RedirectToAction("ThanhCong", "MuaHangs");
            }
            else
            {
                Response.Redirect("~/dang-nhap");
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult ThanhCong()
        {
            return View();
        }
        public ActionResult XemHoaDon()
        {
            if (Session["ID_Account"].Equals("") == false)
            {
                int id = Convert.ToInt32(Session["ID_Account"]);
                var danhsachhoadon = db.HoaDons.Where(x => x.ID_User == id).OrderBy(x => x.Status).OrderByDescending(x => x.NgayBan);
                return View(danhsachhoadon.ToList());
            }
            else
                Response.Redirect("~/dang-nhap");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult HuyHoaDon(FormCollection form)
        {
            if (Session["ID_Account"].Equals("") == false)
            {
                int id = Convert.ToInt32(Session["ID_Account"]);
                int idhoadon = Convert.ToInt32(form["idhoadon"]);
                var hoadon = db.HoaDons.FirstOrDefault(x => x.ID_User == id && x.ID_HoaDon == idhoadon);
                if (hoadon.Status == 0 && hoadon != null)
                {
                    hoadon.Status = 2;
                    db.Entry(hoadon).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            else
                Response.Redirect("~/dang-nhap");
            return RedirectToAction("XemHoaDon");
        }
    }
}