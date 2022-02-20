using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;

namespace WebSenDa.Controllers
{
    public class ThanhToanController : Controller
    {
        SenDaEntities db = new SenDaEntities();

        // ĐƠN HÀNG
        public ActionResult Index(FormCollection form, int id)
        {
            ViewModel model = Session["Cart"] as ViewModel;

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.khachHang = db.KhachHang.Where(x => x.IDKhachHang == id).FirstOrDefault();
            model.diaChi = db.DiaChi.Where(x => x.IDKhachHang == id).FirstOrDefault();

            var listDiaChi = new List<SelectListItem>
            {
                new SelectListItem { Value="0", Text = "All"}
            };

            listDiaChi.AddRange(db.DiaChi
                                    .Select(a => new SelectListItem
                                    {
                                        Value = a.IDDiaChi.ToString(),
                                        Text = a.DiaChi1
                                    }));
            ViewBag.CategoryList = listDiaChi;

            return View(model);

        }
              
        //TẠO ĐƠN HÀNG
        public ActionResult TaoDonHang(FormCollection form )
        {            
            try
            {
                ViewModel model = Session["Cart"] as ViewModel;

                model.ListKhachHang = db.KhachHang.ToArray();
                model.ListDiaChi = db.DiaChi.ToArray();
                //model.khachHang = db.KhachHang.Where(x => x.IDKhachHang == id).FirstOrDefault();
                //model.diaChi = db.DiaChi.Where(x => x.IDKhachHang == id).FirstOrDefault();

                DonHang dh = new DonHang();
                dh.IDKhachHang = (int)Session["idk"];
                dh.NgayDat = DateTime.Now;
                
                dh.DiaChi = form["diachi"];
                dh.TrangThai = 0;
                int pttt;
                string pt = form["phuongthucthanhtoan"];
                if (pt == "1")
                {
                    pttt = 1;
                }
                else
                {
                    pttt = 2;
                }
                dh.PhuongThucThanhToan = pttt;

                db.DonHang.Add(dh);

                foreach (var item in model.Items)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.IDDonHang = dh.IDDonHang;
                    ctdh.IDSanPham = item.sanPham.IDSanPham;
                    ctdh.SoLuong = item.soLuongTon;
                    ctdh.DonGia = (decimal)Session["dongia"];

                    db.ChiTietDonHang.Add(ctdh);

                    foreach (var sp in db.SanPham.Where(s => s.IDSanPham == ctdh.IDSanPham))
                    {
                        var update_quan_pro = sp.Kho.SoLuongTon - item.soLuongTon;
                        sp.Kho.SoLuongTon = update_quan_pro;
                    }
                }
                db.SaveChanges();
                model.ClearCart();
                return RedirectToAction("ThanhToanThanhCong", "ThanhToan");
            }
            catch /*(Exception e)*/
            {
                return /*Content(e.ToString());*/View();
            }
        }

        //THÔNG BÁO THANH TOÁN THÀNH CÔNG
        public ActionResult ThanhToanThanhCong()
        {
            return View();
        }

        //DANH SÁCH ĐƠN HÀNG
        public ActionResult ListDonHang(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }
        //DANH SÁCH ĐƠN CHỜ DUYỆT
        public ActionResult ListDonHangChoDuyet(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }
        //DANH SÁCH ĐƠN ĐÃ DUYỆT
        public ActionResult ListDonHangDaDuyet(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }
        //DANH SÁCH ĐƠN ĐANG VẬN CHUYỂN
        public ActionResult ListDonHangDangVanChuyen(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }
        //DANH SÁCH ĐƠN ĐÃ NHẬN
        public ActionResult ListDonHangDaNhan(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }
        //DANH SÁCH ĐƠN ĐÃ HỦY
        public ActionResult ListDonHangDaHuy(int id)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListDiaChi = db.DiaChi.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.khachHang = db.KhachHang.Where(s => s.IDKhachHang == id).FirstOrDefault();
            model.donHang = db.DonHang.Where(s => s.IDKhachHang == id).FirstOrDefault();

            return View(model);
        }

        //CHI TIẾT ĐƠN HÀNG
        public ActionResult DetailsDonHang(int id)
        {
            var model = new ViewModel();

            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.chiTietDonHang = db.ChiTietDonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.ListKhachHang = db.KhachHang.ToArray();
            model.khachHang = db.KhachHang.Where(x => x.IDKhachHang == model.donHang.IDKhachHang).FirstOrDefault();
            Session["idDonHang"] = id;

            return View(model);
        }

        //HỦY ĐƠN
        public ActionResult HuyDonHang(int id)
        {
            var model = new ViewModel();

            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.chiTietDonHang = db.ChiTietDonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.ListKhachHang = db.KhachHang.ToArray();
            model.khachHang = db.KhachHang.Where(x => x.IDKhachHang == model.donHang.IDKhachHang).FirstOrDefault();

            return View(model);
        }
        [HttpPost]
        public ActionResult HuyDonHang(int id, DonHang dh)
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.chiTietDonHang = db.ChiTietDonHang.Where(s => s.IDDonHang == id).FirstOrDefault();

            if (dh.TrangThai < 2)
            {
                model.donHang.TrangThai = 4;

                foreach (var ctdh in model.ListChiTietDonHang)
                {
                    if (ctdh.IDDonHang == id)
                    {
                        foreach (var sp in db.SanPham.Where(s => s.IDSanPham == ctdh.IDSanPham))
                        {
                            var update_quan_pro = sp.Kho.SoLuongTon + ctdh.SoLuong;
                            sp.Kho.SoLuongTon = update_quan_pro;
                        }
                    }                    
                }
                db.SaveChanges();
                return RedirectToAction("HuyDonThanhCong", "ThanhToan");
            }
            else
            {
                return RedirectToAction("HuyDonThatBai","ThanhToan");
            }            
        }

        //THÔNG BÁO HỦY ĐƠN THẤT BẠI
        public ActionResult HuyDonThatBai()
        {
            return View();
        }

        //THÔNG BÁO HỦY ĐƠN THÀNH CÔNG
        public ActionResult HuyDonThanhCong()
        {
            return View();
        }

        //XÁC NHẬN ĐÃ NHẬN HÀNG
        public ActionResult DaNhanHang(int id)
        {
            var model = new ViewModel();

            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.chiTietDonHang = db.ChiTietDonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.ListKhachHang = db.KhachHang.ToArray();
            model.khachHang = db.KhachHang.Where(x => x.IDKhachHang == model.donHang.IDKhachHang).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult DaNhanHang(int id, DonHang dh)
        {
            var model = new ViewModel();

            model.ListSanPham = db.SanPham.ToArray();
            model.ListDonHang = db.DonHang.ToArray();
            model.ListChiTietDonHang = db.ChiTietDonHang.ToArray();
            model.donHang = db.DonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
            model.chiTietDonHang = db.ChiTietDonHang.Where(s => s.IDDonHang == id).FirstOrDefault();
                        
            model.donHang.TrangThai = 3;
                
            db.SaveChanges();

            return RedirectToAction("Index", "DanhGiaSanPham", new { id = id });

        }     




        // GET: ThanhToan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThanhToan/Delete/5
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
