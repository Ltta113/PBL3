using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;
using PBL3.Models.Model_View;
using System.IO;
using System.Xml.Linq;
using PagedList;

namespace PBL3.Controllers
{
    public class TrangKhuyenMaiController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        // GET: TrangKhuyenMai
        public ActionResult Index(int page = 1)
        {

            TrangKhuyenMai List = new TrangKhuyenMai();
            List<SanPham> sanphams = db.SanPhams.Where(p => p.KhuyenMai.Status == 1).ToList();
            List<KhuyenMai> khuyenmais = db.KhuyenMais.Where(p => p.Status == 1 && p.NoiDungKM != "0").ToList();
            List.ListSP = sanphams;

            int pageSize = 1;
            int pageNumber = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(khuyenmais.Count) /
                Convert.ToDouble(pageSize)));
            int pageSkip = ((int)page - 1) * pageSize;
            ViewBag.Page = page;
            ViewBag.PageNumber = pageNumber;
            khuyenmais = khuyenmais.Skip(pageSkip).Take(pageSize).ToList();
            List.ListKM = khuyenmais;
            return View(List);
        }
        public ActionResult KhuyenMai(string id, int? page)
        {
            ViewBag.ID_KM = id;
            int idkm = Convert.ToInt32(ViewBag.ID_KM);
            if (page == null)
                page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var sanphams = db.SanPhams.Where(p => p.ID_KM == idkm).ToList();
            return View(sanphams.ToPagedList((int)pageNumber, (int)pageSize));
        }
    }
}