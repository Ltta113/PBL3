using PBL3.Models.Model_View;
using PBL3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PBL3.Controllers
{
    public class GioHangsController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        public GioHangs LayGioHang()
        {

            GioHangs giohang = Session["GioHang"] as GioHangs;
            if (giohang == null || Session["GioHang"] == null)
            {
                giohang = new GioHangs();
                Session["GioHang"] = giohang;

            }
            return giohang;


        }
        // Thêm vào giỏ hàng
        public ActionResult ThemVaoGioHang(int id)
        {
            var sanphams = db.SanPhams.FirstOrDefault(x => x.ID_SP == id);
            if (sanphams != null)
            {
                int soluong = 0;               
                if (sanphams.SoLuong != 0)
                {
                    LayGioHang().ThemVaoGio(sanphams);
                    soluong = 1;
                }
                else LayGioHang().ThemVaoGio(sanphams,soluong);
                if (Session["ID_Account"].Equals("") == false)
                {
                    int ma = Convert.ToInt32(Session["ID_Account"]);
                    var themmoi = db.GioHangs.FirstOrDefault(x => x.ID_SP == id && x.ID_GioHang == ma);
                    if (themmoi == null)
                    {
                        db.GioHangs.Add(new GioHang
                        {
                            ID_GioHang = ma,
                            ID_SP = id,
                            SoLuong = soluong

                        });
                        db.SaveChanges();
                    }
                    else
                    {
                        GioHang newgh = db.GioHangs.FirstOrDefault(x => x.ID_GioHang == ma && x.ID_SP == id);
                        newgh.SoLuong += soluong;
                        db.Entry(newgh).State = EntityState.Modified;
                        db.SaveChanges();
                    }  
                }   

            }
            return RedirectToAction("Index", "DanhSachSanPhams"); 
        }
        // Xem giỏ hàng
        public ActionResult XemGioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "DanhSachSanPhams");
            GioHangs giohang = Session["GioHang"] as GioHangs;
            return View(giohang);
        }
        public ActionResult ThayDoiSL(FormCollection form)
        {
            GioHangs giohang = Session["GioHang"] as GioHangs;
            int id = int.Parse(form["idSanPham"]);
            int soluong = int.Parse(form["SoLuongSanPham"]);
            giohang.ThayDoiSoLuong(id, soluong);
            if (Session["ID_Account"].Equals("") == false)
            {
                int ma = Convert.ToInt32(Session["ID_Account"]);
                GioHang newgh = db.GioHangs.FirstOrDefault(x => x.ID_GioHang == ma && x.ID_SP == id);
                newgh.SoLuong = soluong;
                db.Entry(newgh).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("XemGioHang", "GioHangs");
        }
        public ActionResult XoaSanPham(int id)
        {
            GioHangs giohang = Session["GioHang"] as GioHangs;
            giohang.Xoa(id);
            if (Session["ID_Account"].Equals("") == false)
            {
                int ma = Convert.ToInt32(Session["ID_Account"]);
                GioHang newgh = db.GioHangs.FirstOrDefault(x => x.ID_GioHang == ma && x.ID_SP == id);
                db.GioHangs.Remove(newgh);
                db.SaveChanges();
            }
            return RedirectToAction("XemGioHang", "GioHangs");
        }
    }
}