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
        // GET: DangKy
        public ActionResult DangKy()
        {
            if (Session["UserName"].Equals("") == false)
            {
                Response.Redirect("~/Home/Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(DangKy dangky)
        {
            if (ModelState.IsValid)
            {

                var check = db.Accounts.Where(p => p.Username == dangky.Username).FirstOrDefault();
                if (check == null)
                {
                    Account account = new Account();
                    account.Username = dangky.Username;
                    account.Password = dangky.Password;
                    account.Quyen = 1;
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    User user = new User();
                    var newuser = db.Accounts.Where(p => p.Username == dangky.Username).FirstOrDefault();
                    user.ID_User = newuser.ID_Account;
                    user.Ten = dangky.Ten;
                    user.NgaySinh = dangky.NgaySinh;
                    user.SDT = dangky.SDT;
                    user.DiaChi = dangky.DiaChi;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return View();
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

        }
        public ActionResult DangNhap()
        {
            if(Session["UserName"].Equals("") == false)
            {
                Response.Redirect("~/Home/Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {

            Account acc = db.Accounts.FirstOrDefault(x => x.Username == username && x.Quyen == 1);
            if (acc != null)
            {
                if (acc.Password.Replace(" ", "") == password)
                {
                    Session["UserName"] = acc.Username;
                    Session["Password"] = acc.Password;
                    Session["Quyen"] = acc.Quyen;
                    Session["ID_Account"] = acc.ID_Account.ToString();
                    Response.Redirect("~/Home/Index");
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
                Response.Redirect("~/Home/Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(DoiMatKhau dmk)
        {
            if(ModelState.IsValid)
            {
                string mkc = Session["Password"].ToString().Replace(" ","");
                if(dmk.OldPassword.Equals(mkc) == true)
                {
                    string name = Session["Username"].ToString();
                    Session["Password"] = dmk.Password;
                    Account account = db.Accounts.Where(p =>p.Username == name).FirstOrDefault();
                    account.Password = dmk.Password;
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    Response.Redirect("~/Home/Index");
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
            Response.Redirect("~/Home/Index");
            return null;
        }

    }
}