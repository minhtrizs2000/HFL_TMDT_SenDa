using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;
using System.Data.Entity;

namespace WebSenDa.Controllers
{
    public class KhachHangController : Controller
    {
        SenDaEntities db = new SenDaEntities();

        // THÔNG TIN KHÁCH HÀNG
        public ActionResult Details(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.ListDanhGia = db.DanhGia.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }

        //CẬP NHẬT THÔNG TIN KHÁCH HÀNG
        public ActionResult Edit(int id)
        {
            List<LoaiTaiKhoan> listLTK = db.LoaiTaiKhoan.ToList();
            ViewBag.listLTK = new SelectList(listLTK, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan", "");
            return View(db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, KhachHang kh)
        {
            List<LoaiTaiKhoan> listLTK = db.LoaiTaiKhoan.ToList();
            ViewBag.listLTK = new SelectList(listLTK, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan", "");

            if (ModelState.IsValid)
            {
                if (kh.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(kh.UploadImage.FileName);
                    string extent = Path.GetExtension(kh.UploadImage.FileName);
                    filename = filename + extent;
                    kh.AnhDaiDien = "~/Content/img/" + filename;
                    kh.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename));
                }

                // TODO: Add update logic here
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = id });
            }

            return View();
        }

        //DANH SÁCH ĐỊA CHỈ
        public ActionResult IndexDiaChi(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.diaChi = db.DiaChi.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }

        //THÊM ĐỊA CHỈ
        public ActionResult CreateDiaChi()
        {
            var model = new ViewModel();

            int id = (int)Session["IdKH"];
            model.ListKhachHang = db.KhachHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            //model.ListDiaChi = db.DiaChi.ToArray();


            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDiaChi(DiaChi dc, FormCollection form)
        {
            var model = new ViewModel();

            int id = (int)Session["IdKH"];
            model.ListKhachHang = db.KhachHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.ListDiaChi = db.DiaChi.ToArray();
            if (ModelState.IsValid)
            {
                dc.DiaChi1 = form["SoNhaDuong"] + ", " + form["Quan/Huyen"] + ", " + form["Phuong/Xa"] + ", " + form["KhuVuc"];
                dc.IDKhachHang = id;
                db.DiaChi.Add(dc);
                db.SaveChanges();
                return RedirectToAction("IndexDiaChi", new { id = (int)Session["IdKH"] });
            }

            return View(model);
        }

        //CẬP NHẬT ĐỊA CHỈ
        public ActionResult EditDiaChi(int id)
        {
            return View(db.DiaChi.Where(s => s.IDDiaChi == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditDiaChi(int id, DiaChi dc, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                //dc.DiaChi1= form["SoNhaDuong"] + ", " + form["Quan/Huyen"] + ", " + form["Phuong/Xa"] + ", " + form["KhuVuc"];
                db.Entry(dc).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = (int)Session["IdKH"] });
            }

            return View();
        }

        //XÓA ĐỊA CHỈ
        public ActionResult DeleteDiaChi(int id)
        {
            return View(db.DiaChi.Where(p => p.IDDiaChi == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteDiaChi(int id, DiaChi dc)
        {
            try
            {
                // TODO: Add delete logic here
                dc = db.DiaChi.Where(p => p.IDDiaChi == id).FirstOrDefault();
                db.DiaChi.Remove(dc);
                db.SaveChanges();
                return RedirectToAction("IndexDiaChi", new { id = (int)Session["IdKH"] });
            }
            catch
            {
                return View();
            }
        }
    }
}
