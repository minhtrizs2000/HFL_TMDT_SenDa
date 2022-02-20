using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;

namespace WebSenDa.Controllers.QuanLy
{
    public class KhachHang_QLController : Controller
    {
        // GET: KhachHang
        SenDaEntities db = new SenDaEntities();
        public ActionResult Index(string search)
        {
            ViewModel model = new ViewModel();
            if (search == "" || search == null)
            {
                model.ListKhachHang = db.KhachHang.ToList();
                return View("Index", model);

            }
            else
            {
                model.ListKhachHang = db.KhachHang.Where(m => m.TenTaiKhoan.Contains(search)).ToList();
                return View("Index", model);
            }

        }
        public ActionResult Detail(int id)
        {
            ViewModel model = new ViewModel();
            model.khachHang = db.KhachHang.Where(m => m.IDKhachHang == id).SingleOrDefault();
            return View(model);

        }
    }
}