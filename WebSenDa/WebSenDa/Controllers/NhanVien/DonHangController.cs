using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;
using System.Data;
using System.Data.Entity;
namespace WebSenDa.Controllers.NhanVien
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        SenDaEntities db = new SenDaEntities();
        public ActionResult Index(string search)
        {
            ViewModel v = new ViewModel();
            if (search == "0")
            {
                v.ListDonHang = db.DonHang.Where(m => m.TrangThai == 0).ToList();
                return View("Index", v);

            }
            else if (search == "1")
            {
                v.ListDonHang = db.DonHang.Where(m => m.TrangThai == 1).ToList();
                return View("Index", v);

            }
            else if (search == "2")
            {
                v.ListDonHang = db.DonHang.Where(m => m.TrangThai == 2).ToList();
                return View("Index", v);

            }
            else if (search == "3")
            {
                v.ListDonHang = db.DonHang.Where(m => m.TrangThai == 3).ToList();
                return View("Index", v);

            }
            else if (search == "4")
            {
                v.ListDonHang = db.DonHang.Where(m => m.TrangThai == 4).ToList();
                return View("Index", v);
            }
            else
            {
                v.ListDonHang = db.DonHang.ToList();
                return View("Index", v);


            }


        }
        public ActionResult Detail(int id, string giao, string duyet, string huy, string dangGiao)
        {
            ViewModel v = new ViewModel();
            v.donHang = db.DonHang.Where(m => m.IDDonHang == id).Single();
            v.ListChiTietDonHang = db.ChiTietDonHang.Where(m => m.IDDonHang == id).ToList();
            if (!string.IsNullOrEmpty(duyet))
            {
                DonHang dh = new DonHang();
                dh = db.DonHang.Where(m => m.IDDonHang == id).SingleOrDefault();
                dh.TrangThai = 1;
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(huy))
            {
                DonHang dh = new DonHang();
                dh = db.DonHang.Where(m => m.IDDonHang == id).SingleOrDefault();
                dh.TrangThai = 4;
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(giao))
            {
                DonHang dh = new DonHang();
                dh = db.DonHang.Where(m => m.IDDonHang == id).Single();
                dh.TrangThai = 2;
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(dangGiao))
            {
                DonHang dh = new DonHang();
                dh = db.DonHang.Where(m => m.IDDonHang == id).Single();
                dh.TrangThai = 3;
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(v);
        }


    }
}