using PBL3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3.Areas.Admin.Controllers
{
    public class HomePageController : Controller
    {
        // GET: Admin/HomePage
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (Session["Quyen"].Equals("0") == true )
            {
                Response.Redirect("~/Admin/HomePage");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            Account acc = db.Accounts.FirstOrDefault(x => x.Username == username && x.Quyen == 0);
            string err = "";
            if (acc != null)
            {
                if (acc.Password.Replace(" ", "") == password)
                {
                    Session["UserName"] = acc.Username;
                    Session["Password"] = acc.Password;
                    Session["Quyen"] = acc.Quyen.ToString();
                    Session["ID_Account"] = acc.ID_Account.ToString();
                    Response.Redirect("~/Admin/HomePage");
                }
                else ViewBag.Error = "<p class='text-danger'> " + "Mật khẩu không hợp lệ" + "</p>";
            }
            else
            {
                ViewBag.Error = "<p class='text-danger'> " + "Tên đăng nhập không hợp lệ" + "</p>";
            }
            
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserName"] = "";
            Session["Password"] = "";
            Session["Quyen"] = "";
            Session["ID_Account"] = "";
            Session["GioHang"] = null;
            Session["Name"] = "";
            Response.Redirect("~/Admin/HomePage/Login");
            return null;
        }
    }
}