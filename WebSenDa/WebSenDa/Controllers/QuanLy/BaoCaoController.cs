using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;

namespace WebSenDa.Controllers.QuanLy
{
    public class BaoCaoController : Controller
    {

        SenDaEntities db = new SenDaEntities();

        // GET: BaoCao
        public ActionResult Index()
        {
            var model = new ViewModel();

            //kho
            model.ListNhapKho = db.NhapKho.Where(s=>s.NgayNhap.Year == DateTime.Now.Year).ToArray();

            //đơn hàng
            model.ListDonHang = db.DonHang.Where(s=>s.NgayDat.Year == DateTime.Now.Year).ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();

            return View(model);
        }
        public ActionResult LocNam(int inputYear)
        {
            var model = new ViewModel();

            //Kho
            model.ListNhapKho = db.NhapKho.Where(s => s.NgayNhap.Year == inputYear).ToArray();

            //Đơn hàng
            model.ListDonHang = db.DonHang.Where(s => s.NgayDat.Year == inputYear).ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.Year = inputYear;
            return View(model);
        }
        public ActionResult Details(int month, int year)
        {
            var model = new ViewModel();

            //Sản phẩm
            model.ListSanPham = db.SanPham.ToArray();

            //Kho
            model.ListNhapKho = db.NhapKho.Where(s => s.NgayNhap.Year == year && s.NgayNhap.Month == month).ToArray();

            //Đơn hàng
            model.ListDonHang = db.DonHang.Where(s => s.NgayDat.Year == year && s.NgayDat.Month == month).ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();

            //Date
            model.Year = year;
            model.Month = month;

            return View(model);
        }
    }
}