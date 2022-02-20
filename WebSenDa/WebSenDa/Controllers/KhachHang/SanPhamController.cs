using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSenDa.Models;
using PagedList;


namespace WebSenDa.Controllers
{
    public class SanPhamController : Controller
    {
        SenDaEntities db = new SenDaEntities();
        // GET: SanPham
        public ActionResult Index()
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiSanPham = db.LoaiSanPham.ToArray();
            model.ListLoaiSenDa = db.LoaiSenDa.ToArray();
            model.ListLoaiChauCay = db.LoaiChauCay.ToArray();
            model.ListLoaiGiaThe = db.LoaiGiaThe.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();

            return View(model);
        }

        //Get: Sanpham/LocLoaiSP/idLoaiSP
        public ActionResult LocLoaiSP(int id)
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiSanPham = db.LoaiSanPham.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();
            model.sanPham = db.SanPham.Where(x => x.IDLoaiSanPham == id).FirstOrDefault();

            return View(model);
        }
        public ActionResult LocLoaiSD(int id)
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiSenDa = db.LoaiSenDa.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();
            model.sanPham = db.SanPham.Where(x => x.IDLoaiSenDa == id ).FirstOrDefault();

            return View(model);
        }
        public ActionResult LocLoaiCC(int id)
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiChauCay = db.LoaiChauCay.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();
            model.sanPham = db.SanPham.Where(x => x.IDLoaiChauCay == id).FirstOrDefault();

            return View(model);
        }
        public ActionResult LocLoaiGT(int id)
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiGiaThe = db.LoaiGiaThe.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();
            model.sanPham = db.SanPham.Where(x => x.IDLoaiGiaThe == id).FirstOrDefault();

            return View(model);
        }
        //Get: Sanpham/LocGiaSP/
        public ActionResult LocGiaSP(double min = double.MinValue, double max = double.MaxValue)
        {
            var model = new ViewModel();
            
            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiSanPham = db.LoaiSanPham.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();
            model.ListSanPham = db.SanPham.Where(x => (double)x.Kho.GiaBan >= min && (double)x.Kho.GiaBan <= max).ToArray();

            return View(model);

            //var list = database.MauXes.Where(p => (double)p.GiaBan >= min && (double)p.GiaBan <= max).ToArray();
            //return View(list);
        }
        //Get: SanPham/AllSP
        public ActionResult AllSP(string searchString, double min = double.MinValue, double max = double.MaxValue)
        {               
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiSanPham = db.LoaiSanPham.ToArray();            
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();            
            model.ListSanPham = db.SanPham.Where(s => (double)s.Kho.GiaBan >= min && (double)s.Kho.GiaBan <= max
                                                    || s.TenSanPham.Contains(searchString)
                                                    || s.LoaiSanPham.TenLoaiSanPham.Contains(searchString)
                                                    || s.LoaiSenDa.TenLoaiSenDa.Contains(searchString)
                                                    || s.LoaiChauCay.TenLoaiChauCay.Contains(searchString)
                                                    || s.LoaiGiaThe.TenLoaiGiaThe.Contains(searchString)).ToArray();
            

            return View(model);
        }

        public ActionResult SearchSP(string searchString)
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiSanPham = db.LoaiSanPham.ToArray();
            model.ListLoaiSenDa = db.LoaiSenDa.ToArray();
            model.ListLoaiChauCay = db.LoaiChauCay.ToArray();
            model.ListLoaiGiaThe = db.LoaiGiaThe.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();
            model.ListDanhGia = db.DanhGia.ToArray();            
            model.ListSanPham = db.SanPham.Where(s => s.TenSanPham.Contains(searchString)
                                                || s.LoaiSanPham.TenLoaiSanPham.Contains(searchString)
                                                || s.LoaiSenDa.TenLoaiSenDa.Contains(searchString)
                                                || s.LoaiChauCay.TenLoaiChauCay.Contains(searchString)
                                                || s.LoaiGiaThe.TenLoaiGiaThe.Contains(searchString));

            return View(model);             
        }

        // GET: SanPham/Details/idSP
        public ActionResult Details(int id)
        {

            var model = new ViewModel();
            model.ListSanPham = db.SanPham.ToArray();
            model.ListLoaiSanPham = db.LoaiSanPham.ToArray();
            model.ListLoaiSenDa = db.LoaiSenDa.ToArray();
            model.ListLoaiChauCay = db.LoaiChauCay.ToArray();
            model.ListLoaiGiaThe = db.LoaiGiaThe.ToArray();
            model.ListKhuyenMai = db.KhuyenMai.ToArray();
            model.ListKho = db.Kho.ToArray();
            model.ListNhapKho = db.NhapKho.ToArray();
            model.ListDanhGia = db.DanhGia.ToArray();
            model.sanPham = db.SanPham.Where(x => x.IDSanPham == id).FirstOrDefault();

            return View(model);
        }

        public ActionResult kho()
        {
            var model = new ViewModel();

            model.ListKho = db.Kho.ToArray();
            return View(model);
        }
        

        private static Expression<Func<SanPham, bool>> NewMethod(int id)
        {
            return model => model.IDSanPham == id;
        }

        // GET: SanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanPham/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SanPham/Edit/idSP
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SanPham/Edit/idSP
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SanPham/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SanPham/Delete/idSP
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }    
}
