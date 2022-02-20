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
    public class QuyenController : Controller
    {
        // GET: Quyen
        SenDaEntities db = new SenDaEntities();
        // GET: NhanVien
        public ActionResult Index()
        {
            var model = new ViewModel();
            model.ListQuyen = db.Quyen.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new ViewModel();
            model.quyen = new Quyen();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModel v)
        {
            var check = db.Quyen.Where(s => s.IDQuyen == v.quyen.IDQuyen).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (check == null)
                {
                    Quyen q = new Quyen();
                    q = v.quyen;
                    db.Quyen.Add(q);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.Error = "Mã quyền đã tồn tại";
                    return View();
                }

            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = new ViewModel();
            Quyen q = db.Quyen.Where(m => m.IDQuyen == id).SingleOrDefault();
            model.quyen = q;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ViewModel v)
        {
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Quyen q = db.Quyen.Where(m => m.IDQuyen == v.quyen.IDQuyen).SingleOrDefault();
                q.TenQuyen = v.quyen.TenQuyen;
                db.Entry(q).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}