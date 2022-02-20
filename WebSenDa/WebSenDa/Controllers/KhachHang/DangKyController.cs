using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;

namespace WebSenDa.Controllers
{
    public class DangKyController : Controller
    {
        SenDaEntities db = new SenDaEntities();
        // GET: DangKy
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TaiKhoanModel tkmodel,KhachHang kh)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListLoaiTaiKhoan = db.LoaiTaiKhoan.ToArray();

            var check = db.KhachHang.Where(s => s.TenTaiKhoan == tkmodel.TenTaiKhoan && s.Email == tkmodel.Email).FirstOrDefault();
            if (check == null)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                kh.IDLoaiTaiKhoan = 3;
                kh.DiemTichLuy = 0;
                kh.TenTaiKhoan = tkmodel.TenTaiKhoan;
                kh.MatKhau = tkmodel.MatKhau;
                kh.Email = tkmodel.Email;
                kh.SoDienThoai = tkmodel.SoDienThoai;
                kh.CMND = tkmodel.CMND;
                db.KhachHang.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index","DangNhap");
            }
            else
            {
                ViewBag.ErrorRegister = "This ID is exixst";
                return View();
            }
        }

        // GET: DangKy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DangKy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DangKy/Create
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

        // GET: DangKy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DangKy/Edit/5
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

        // GET: DangKy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DangKy/Delete/5
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
