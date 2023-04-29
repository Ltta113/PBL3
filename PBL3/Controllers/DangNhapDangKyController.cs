using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;
using PBL3.Models.Model_View;

namespace PBL3.Controllers
{
    public class DangNhapDangKyController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        //GET: DangKy
        public ActionResult DangKy()
        {
            if (Session["UserName"].Equals("") == false)
            {
                Response.Redirect("~/Trang-chu");
            }

            return View("DangKy");
        }

        [HttpPost]
        public ActionResult DangKy(DangKy dangky)
        {
            if (ModelState.IsValid)
            {

                var check = db.Accounts.Where(p => p.Username == dangky.Username.Replace(" ", "")).FirstOrDefault();
            if (check == null)
            {
                Account account = new Account();
                account.Username = dangky.Username.Replace(" ", "");
                account.Password = dangky.Password;
                account.Quyen = 1;
                db.Accounts.Add(account);
                User user = new User();
                user.ID_User = account.ID_Account;
                user.Ten = dangky.Ten;
                user.NgaySinh = dangky.NgaySinh;
                user.GioiTinh = dangky.GioiTinh;
                user.SDT = dangky.SDT;
                user.DiaChi = dangky.DiaChi;
                db.Users.Add(user);
                db.SaveChanges();

                Response.Redirect("~/dang-nhap");
            }
            else
            {
                ViewBag.Error = "<p class='text-danger'> " + "  Tên đăng nhập đã tồn tại, mời nhập lại" + "</p>";
                return View();
            }
            }

            else
            {
                ViewBag.Error = "<p class='text-danger'> " + "  Lỗi dữ liệu" + "</p>";
                return View();
            }
            return View();

        }

        public ActionResult DangNhap()
        {
            if (Session["UserName"].Equals("") == false)
            {
                Response.Redirect("~/Trang-chu");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {

            Account acc = db.Accounts.FirstOrDefault(x => x.Username == username && x.Quyen == 1);

            if (acc != null)
            {
                User user = db.Users.FirstOrDefault(x => x.ID_User == acc.ID_Account);
                if (acc.Password.Replace(" ", "") == password)
                {
                    Session["UserName"] = acc.Username;
                    Session["Password"] = acc.Password;
                    Session["Quyen"] = acc.Quyen.ToString();
                    Session["Name"] = user.Ten;
                    Session["ID_Account"] = acc.ID_Account.ToString();
                    GioHangs giohang = Session["GioHang"] as GioHangs;
                    if (giohang == null || Session["GioHang"] == null)
                    {
                        giohang = new GioHangs();
                        Session["GioHang"] = giohang;
                        Session["SoLuong"] = 0;
                    }
                    foreach (var item in giohang.Items)
                    {
                        int ma = Convert.ToInt32(Session["ID_Account"]);
                        var themmoi = db.GioHangs.FirstOrDefault(x => x.ID_SP == item.SanPhamGioHang.ID_SP && x.ID_GioHang == ma);
                        if (themmoi == null)
                        {
                            db.GioHangs.Add(new GioHang
                            {
                                ID_GioHang = ma,
                                ID_SP = item.SanPhamGioHang.ID_SP,
                                SoLuong = item.SoLuong

                            });
                            db.SaveChanges();
                        }
                        else
                        {
                            themmoi.SoLuong += item.SoLuong;
                            db.Entry(themmoi).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    giohang.Huy();
                    giohang = new GioHangs();
                    var sanphams = db.GioHangs.Where(x => x.ID_GioHang == acc.ID_Account);
                    if (sanphams != null)
                    {
                        foreach (var item in sanphams)
                        {
                            var sp = db.SanPhams.FirstOrDefault(x => x.ID_SP == item.ID_SP);
                            int soluong = Convert.ToInt32(item.SoLuong);
                            giohang.ThemVaoGio(sp, soluong);

                        }
                        if (giohang.Items.Count() == 0)
                            Session["GioHang"] = null;
                        else
                            Session["GioHang"] = giohang;
                        Session["SoLuong"] = giohang.Dem();
                    }
                    Response.Redirect("~/Trang-chu");
                }
                else ViewBag.Error = "<p class='text-danger'> " + "Mật khẩu không hợp lệ" + "</p>";
            }
            else
            {
                ViewBag.Error = "<p class='text-danger'> " + "Tên đăng nhập không hợp lệ" + "</p>";
            }
            return View();
        }
        public ActionResult DoiMatKhau()
        {
            if (Session["UserName"].Equals("") == true)
            {
                Response.Redirect("~/Trang-chu");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(DoiMatKhau dmk)
        {
            if (ModelState.IsValid)
            {
                string mkc = Session["Password"].ToString().Replace(" ", "");
                if (dmk.OldPassword.Equals(mkc) == true)
                {
                    string name = Session["Username"].ToString();
                    Session["Password"] = dmk.Password;
                    Account account = db.Accounts.Where(p => p.Username == name).FirstOrDefault();
                    account.Password = dmk.Password;
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    Response.Redirect("~/Trang-chu");
                }
                else ViewBag.Error = "<p class='text-danger'> " + "Mật khẩu cũ không không đúng" + "</p>";

            }
            else ViewBag.Error = "<p class='text-danger'> " + "Lỗi dữ liệu" + "</p>";
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["UserName"] = "";
            Session["Password"] = "";
            Session["Quyen"] = "";
            Session["ID_Account"] = "";
            Session["GioHang"] = null;
            Response.Redirect("~/Trang-chu");
            return null;
        }
        public ActionResult ThongTinCaNhan()
        {
            if (Session["UserName"].Equals("") == true)
            {
                Response.Redirect("~/Trang-chu");
            }
            else
            {
                int id = Convert.ToInt32(Session["ID_Account"]);
                var model = db.Users.Where(x => x.ID_User == id).FirstOrDefault();
                return View(model);
            }
            return View();
        }
        public ActionResult ThayDoiThongTin()
        {
            if (Session["UserName"].Equals("") == true)
            {
                Response.Redirect("~/Trang-chu");
            }
            else
            {
                int id = Convert.ToInt32(Session["ID_Account"]);
                var model = db.Users.Where(x => x.ID_User == id).FirstOrDefault();
                DateTime datetime = (DateTime)model.NgaySinh;
                string date = datetime.ToString("MM/dd/yyyy");
                ViewBag.NgaySinh = date;
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThayDoiThongTin([Bind(Include = "ID_User,Ten,NgaySinh,DiaChi,GioiTinh,SDT")] User user)
        {

            if (ModelState.IsValid)
            {
                Session["Name"] = user.Ten;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Response.Redirect("~/Thong-tin-ca-nhan");
            }
            ViewBag.ID_User = new SelectList(db.Accounts, "ID_Account", "Username", user.ID_User);

            return View(user);
        }
    }
}