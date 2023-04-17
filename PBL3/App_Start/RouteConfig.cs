using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PBL3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                name: "Dang-nhap",
                url: "dang-nhap",
                defaults: new { controller = "DangNhapDangKy", action = "DangNhap", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Dang-ky",
                url: "dang-ky",
                defaults: new { controller = "DangNhapDangKy", action = "DangKy", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Doi-mat-khau",
                url: "doi-mat-khau",
                defaults: new { controller = "DangNhapDangKy", action = "DoiMatKhau", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Dang-xuat",
                url: "dang-xuat",
                defaults: new { controller = "DangNhapDangKy", action = "DangXuat", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
