using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;

namespace WebSenDa.Controllers
{
    public class DanhGiaSanPhamController : Controller
    {
        SenDaEntities db = new SenDaEntities();
        
        //DANH GIÁ SẢN PHẨM
        public ActionResult Index(int id)
        {
            var model = new ViewModel();

            //int id = (int)Session["idDonHang"];
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.chiTietDonHang = db.ChiTietDonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.ListDanhGia = db.DanhGia.ToArray();
            return View(model);
        }

        //TẠO ĐÁNH GIÁ
        public ActionResult Create(int id, int idDonHang)
        {
            var model = new ViewModel();

            DanhGia danhGia = new DanhGia();
            model.ListDanhGia = db.DanhGia.ToArray();
            model.ListSanPham = db.SanPham.ToArray();
            model.ListKhachHang = db.KhachHang.ToArray();
            model.sanPham = db.SanPham.Where(s => s.IDSanPham == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == idDonHang).FirstOrDefault();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(int id, DanhGia dg,FormCollection form)
        {
            var model = new ViewModel();

            int idkh = (int)Session["IdKH"];

            model.ListDanhGia = db.DanhGia.ToArray();
            model.ListSanPham = db.SanPham.ToArray();
            model.ListKhachHang = db.KhachHang.ToArray();
            model.sanPham = db.SanPham.Where(s => s.IDSanPham == id).FirstOrDefault();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == idkh).FirstOrDefault();

            model.danhGia = db.DanhGia.Where(s => s.IDKhachHang == idkh && s.IDSanPham == id).FirstOrDefault();

            try
            {
                    if (dg.UploadImage != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(dg.UploadImage.FileName);
                        string extent = Path.GetExtension(dg.UploadImage.FileName);
                        filename = filename + extent;
                        dg.Hinh = "~/Content/img/" + filename;
                        dg.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename));
                    }

                    if (dg.UploadImage1 != null)
                    {
                        string filename1 = Path.GetFileNameWithoutExtension(dg.UploadImage1.FileName);
                        string extent1 = Path.GetExtension(dg.UploadImage1.FileName);
                        filename1 = filename1 + extent1;
                        dg.Hinh1 = "~/Content/img/" + filename1;
                        dg.UploadImage1.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename1));
                    }

                    if (dg.UploadImage2 != null)
                    {
                        string filename2 = Path.GetFileNameWithoutExtension(dg.UploadImage2.FileName);
                        string extent2 = Path.GetExtension(dg.UploadImage2.FileName);
                        filename2 = filename2 + extent2;
                        dg.Hinh2 = "~/Content/img/" + filename2;
                        dg.UploadImage2.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename2));
                    }

                    dg.NoiDung = form["NoiDung"];
                    if (dg.NoiDung == "")
                    {
                        dg.NoiDung = "Tôi Rất Hài Lòng Về Sản Phẩm!!!";
                    }
                    dg.IDSanPham = id;
                    dg.IDKhachHang = idkh;
                    dg.NgayDanhGia = DateTime.Now;

                    db.DanhGia.Add(dg);
                    db.SaveChanges();

                    return RedirectToAction("DanhGiaThanhCong", "DanhGiaSanPham", new { id = (int)Session["iddh"] });
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        //SỬA ĐÁNH GIÁ
        public ActionResult EditDanhGia(int id, int idDonHang)
        {
            var model = new ViewModel();

            int idkh = (int)Session["IdKH"];

            model.ListDanhGia = db.DanhGia.ToArray();
            model.ListSanPham = db.SanPham.ToArray();
            model.ListKhachHang = db.KhachHang.ToArray();

            model.sanPham = db.SanPham.Where(s => s.IDSanPham == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == idDonHang).FirstOrDefault();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == idkh).FirstOrDefault();
            model.danhGia = db.DanhGia.Where(s => s.IDKhachHang == idkh && s.IDSanPham == id).FirstOrDefault();

            return View(model);
        }
        [HttpPost]
        public ActionResult EditDanhGia(int id/*, int idDonHang*/, DanhGia dg, FormCollection form)
        {
            var model = new ViewModel();

            int idkh = (int)Session["IdKH"];

            model.ListDanhGia = db.DanhGia.ToArray();
            model.ListSanPham = db.SanPham.ToArray();
            model.ListKhachHang = db.KhachHang.ToArray();

            model.sanPham = db.SanPham.Where(s => s.IDSanPham == id).FirstOrDefault();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == idkh).FirstOrDefault();
            model.danhGia = db.DanhGia.Where(s => s.IDKhachHang == idkh && s.IDSanPham == id).FirstOrDefault();

            try
            {
                if (dg.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(dg.UploadImage.FileName);
                    string extent = Path.GetExtension(dg.UploadImage.FileName);
                    filename = filename + extent;
                    model.danhGia.Hinh = dg.Hinh = "~/Content/img/" + filename;
                    dg.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename));
                    model.danhGia.UploadImage = dg.UploadImage;
                }

                if (dg.UploadImage1 != null)
                {
                    string filename1 = Path.GetFileNameWithoutExtension(dg.UploadImage1.FileName);
                    string extent1 = Path.GetExtension(dg.UploadImage1.FileName);
                    filename1 = filename1 + extent1;
                    model.danhGia.Hinh1 = dg.Hinh1 = "~/Content/img/" + filename1;
                    dg.UploadImage1.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename1));
                    model.danhGia.UploadImage1 = dg.UploadImage1;
                }

                if (dg.UploadImage2 != null)
                {
                    string filename2 = Path.GetFileNameWithoutExtension(dg.UploadImage2.FileName);
                    string extent2 = Path.GetExtension(dg.UploadImage2.FileName);
                    filename2 = filename2 + extent2;
                    model.danhGia.Hinh2 = dg.Hinh2 = "~/Content/img/" + filename2;
                    dg.UploadImage2.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename2));
                    model.danhGia.UploadImage2 = dg.UploadImage2;
                }


                dg.NoiDung = form["NoiDung"];
                if (dg.NoiDung != "")
                {
                    model.danhGia.NoiDung = dg.NoiDung;
                }
                model.danhGia.NgayDanhGia = dg.NgayDanhGia = DateTime.Now;

                db.Entry(model.danhGia).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DanhGiaThanhCong", "DanhGiaSanPham", new { id = (int)Session["iddh"] });
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return View(model);
        }

        //ĐÁNH GIÁ THÀNH CÔNG
        public ActionResult DanhGiaThanhCong()
        {
            return View();
        }
    }
}
