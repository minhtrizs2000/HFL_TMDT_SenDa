using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSenDa.Models;

namespace WebSenDa.Controllers
{
    public class GioHangController : Controller
    {
        SenDaEntities db = new SenDaEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            //var model = new ViewModel();

            //model.ListKhachHang = db.KhachHang.ToArray();

            if (Session["Cart"] == null)
                return View("EmptyCart");
            ViewModel cart = Session["Cart"] as ViewModel;
            cart.ListKhachHang = db.KhachHang.ToArray();
            return View(cart);
        }
        public ViewModel GetCart()
        {
            ViewModel cart = Session["Cart"] as ViewModel;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new ViewModel();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var sp = db.SanPham.SingleOrDefault(s => s.IDSanPham == id);
            if (sp != null)
            {
                GetCart().Add_Product_Cart(sp);
            }
            return RedirectToAction("Index", "GioHang");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            ViewModel cart = Session["Cart"] as ViewModel;
            int idsp = int.Parse(form["iDSanPham"]);
            int soLuongTon = int.Parse(form["soLuongTon"]);
            cart.Update_quantity(idsp, soLuongTon);

            return RedirectToAction("Index", "GioHang");
        }
        public ActionResult RemoveCart(int id)
        {
            ViewModel cart = Session["Cart"] as ViewModel;
            cart.Remove_CartItem(id);
            return RedirectToAction("Index", "GioHang");
        }
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            ViewModel cart = Session["Cart"] as ViewModel;
            if (cart != null)
                total_quantity_item = cart.Total_quantity();
            ViewBag.Quantity = total_quantity_item;
            return PartialView("BagCart");
        }
    }
}
