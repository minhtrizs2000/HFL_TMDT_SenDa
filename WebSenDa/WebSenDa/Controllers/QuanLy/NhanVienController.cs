using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;
using System.Data;
using System.Data.Entity;

namespace WebSenDa.Controllers.QuanLy
{
    public class NhanVienController : Controller
    {

        SenDaEntities db = new SenDaEntities();
        // GET: NhanVien
        public ActionResult Index(string search)
        {


            ViewModel v = new ViewModel();
            if (search == "" || search == null)
            {
                v.ListNhanVien = db.NhanVien.Where(m => m.IDQuyen != 4).ToList();
                return View("Index", v);
            }
            else
            {
                v.ListNhanVien = db.NhanVien.Where(m => m.TenNhanVien.Contains(search) && m.IDQuyen != 4).ToList();
                return View("Index", v);
            }
        }
        public ActionResult Details(int id)
        {
            var model = new ViewModel();
            model.nhanVien = db.NhanVien.Where(m => m.IDNhanVien == id).SingleOrDefault();

            return View(model);
        }
        public ActionResult Create()
        {
            var model = new ViewModel();

            List<LoaiTaiKhoan> list = db.LoaiTaiKhoan.ToList();
            ViewBag.listLoaiTK = new SelectList(list, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan", 1);
            List<Quyen> listQ = db.Quyen.ToList();
            ViewBag.listQuyen = new SelectList(listQ, "IDQuyen", "TenQuyen", 1);

            model.nhanVien = new Models.NhanVien();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModel v)
        {
            Models.NhanVien nv = new Models.NhanVien();
            var check = db.NhanVien.Where(s => s.IDNhanVien == v.nhanVien.IDNhanVien).FirstOrDefault();
            var check_tk = db.NhanVien.Where(s => s.TenTaiKhoan == v.nhanVien.TenTaiKhoan).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (check == null && check_tk == null)
                {
                    List<LoaiTaiKhoan> list = db.LoaiTaiKhoan.ToList();
                    ViewBag.listLoaiTK = new SelectList(list, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan", nv.IDLoaiTaiKhoan);
                    List<Quyen> listQ = db.Quyen.ToList();
                    ViewBag.listQuyen = new SelectList(listQ, "IDQuyen", "TenQuyen", nv.IDQuyen);
                    nv = v.nhanVien;
                    db.NhanVien.Add(nv);
                    db.SaveChanges();
                    return RedirectToAction("Index", "NhanVien");

                }
                else
                {
                    ViewBag.Error = "Mã nhân viên hoặc tên tài khoản đã tồn tại";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = new ViewModel();
            Models.NhanVien nv = db.NhanVien.Where(n => n.IDNhanVien == id).SingleOrDefault();
            //ViewBag.IDLoaiTaiKhoan = new SelectList(db.LoaiTaiKhoan, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan",nv.IDLoaiTaiKhoan);
            //ViewBag.IDQuyen = new SelectList(db.Quyen, "IDQuyen", "TenQuyen",nv.IDQuyen);
            List<LoaiTaiKhoan> list = db.LoaiTaiKhoan.ToList();
            ViewBag.listLoaiTK = new SelectList(list, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan", nv.IDLoaiTaiKhoan);
            List<Quyen> listQ = db.Quyen.ToList();
            ViewBag.listQuyen = new SelectList(listQ, "IDQuyen", "TenQuyen", nv.IDQuyen);
            model.nhanVien = nv;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ViewModel v)
        {
            Models.NhanVien nv = db.NhanVien.Where(m => m.IDNhanVien == v.nhanVien.IDNhanVien).SingleOrDefault();
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                nv.TenNhanVien = v.nhanVien.TenNhanVien;
                /*            nv.IDLoaiTaiKhoan = int.Parse(v.nhanvien.IDLoaiTaiKhoan.ToString());
                */
                nv.IDQuyen = int.Parse(v.nhanVien.IDQuyen.ToString());
                nv.AnhDaiDien = v.nhanVien.Email;
                nv.IDLoaiTaiKhoan = v.nhanVien.IDLoaiTaiKhoan;
                nv.SoDienThoai = v.nhanVien.SoDienThoai;
                nv.TenTaiKhoan = v.nhanVien.TenTaiKhoan;
                nv.MatKhau = v.nhanVien.MatKhau;
                nv.CMND = v.nhanVien.CMND;
                nv.Email = v.nhanVien.Email;
                nv.AnhDaiDien = v.nhanVien.AnhDaiDien;
                db.NhanVien.Attach(nv);
                db.Entry(nv).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.IDLoaiTaiKhoan = new SelectList(db.LoaiTaiKhoan, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan", nv.IDLoaiTaiKhoan);
                //ViewBag.IDQuyen = new SelectList(db.Quyen, "IDQuyen", "TenQuyen", nv.IDQuyen);
                List<LoaiTaiKhoan> list = db.LoaiTaiKhoan.ToList();
                ViewBag.listLoaiTK = new SelectList(list, "IDLoaiTaiKhoan", "TenLoaiTaiKhoan", nv.IDLoaiTaiKhoan);
                List<Quyen> listQ = db.Quyen.ToList();
                ViewBag.listQuyen = new SelectList(listQ, "IDQuyen", "TenQuyen", nv.IDQuyen);
                return RedirectToAction("Index", "NhanVien");

            }
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int submit)
        {
            Models.NhanVien nv = db.NhanVien.Where(m => m.IDNhanVien == submit).SingleOrDefault();
            nv.IDQuyen = 4;
            db.SaveChanges();
            return Redirect("Index");
        }
        //public ActionResult SelectQuyen()
        //{
        //    Quyen r = new Quyen();
        //    r.listRole = db.Quyen.ToList<Quyen>();
        //    return PartialView(r);
        //}
    }
}