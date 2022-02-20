using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSenDa.Models;

namespace WebSenDa.Controllers
{
    public class DangNhapController : Controller
    {
        SenDaEntities db = new SenDaEntities();
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TaiKhoanModel tkmodel)
        {
            var model = new ViewModel();

            model.ListKhachHang = db.KhachHang.ToArray();
            model.ListNhanVien = db.NhanVien.ToArray();

            var check_KH = db.KhachHang.Where(s => s.Email == tkmodel.Email && s.MatKhau == tkmodel.MatKhau).FirstOrDefault();
            var check_NV = db.NhanVien.Where(s => s.Email == tkmodel.Email && s.MatKhau == tkmodel.MatKhau).SingleOrDefault();

            if (check_NV != null) //kiểm tra có phải NV k
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["Email"] = check_NV.Email;
                Session["TenTaiKhoan"] = check_NV.TenTaiKhoan;
                Session["IdNV"] = check_NV.IDNhanVien;
                model.nhanVien = check_NV;

                if (check_NV.IDQuyen == 1) //ADIM
                {
                    Session["IdQuyen"] = 1;
                    return RedirectToAction("Index", "Quyen", model);
                }
                else if (check_NV.IDQuyen == 2) //NV KHO
                {
                    Session["IdQuyen"] = 2;
                    return RedirectToAction("Index", "QLSanPham", model);
                }
                else if (check_NV.IDQuyen == 3) //NV BÁN HÀNG
                {
                    Session["IdQuyen"] = 3;
                    return RedirectToAction("Index", "DonHang", model);
                }
                else // id=4 => Không hoạt động
                {
                    ViewBag.error = "Tài khoản của bạn đã bị vô hiệu hóa";
                    return View("Index");
                }
            }
            else if (check_KH != null) //Kiểm tra có phải KH k
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["Email"] = check_KH.Email;
                Session["IdKH"] = check_KH.IDKhachHang;
                Session["TenTK"] = check_KH.TenTaiKhoan;
                return RedirectToAction("Index", "SanPham");
            }

            ViewBag.error = "Sai thông tin đăng nhập";
            return View("Index");
        }

        public ActionResult DangXuat()
        {
            Session.Abandon();
            return RedirectToAction("Index", "SanPham");
        }

        [HttpGet]
        public ActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Reset(ResetModel resetModel)
        {

            if (ModelState.IsValid)
            {
                var modelKH = await db.KhachHang.Where(x => x.Email == resetModel.Email).FirstOrDefaultAsync();
                var modelNV = await db.NhanVien.Where(x => x.Email == resetModel.Email).FirstOrDefaultAsync();

                if (modelKH != null)
                {
                    //model.pass đã được set new password
                    modelKH.MatKhau = GetPasswordRandom();
                    //db.KhachHang.Update(model);
                    db.Entry(modelKH).State = EntityState.Modified;

                    await db.SaveChangesAsync();

                    #region Send mail
                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("Admin", "hoanglong29032000@gmail.com");
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress("User", modelKH.Email);
                    message.To.Add(to);

                    message.Subject = "Reset Mật khẩu thành công";
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $"<h1>Mật khẩu của bạn đã được reset, mật khẩu mới: {modelKH.MatKhau}  </h1>";
                    bodyBuilder.TextBody = "Mật Khẩu của bạn đã được thay đổi ";
                    message.Body = bodyBuilder.ToMessageBody();
                    // xac thuc email
                    SmtpClient client = new SmtpClient();
                    //connect (smtp address, port , true)
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("hoanglong29032000@gmail.com", "qihdtmmuqslqyaro");
                    //send email
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                    #endregion

                    return View("Index");
                }
                else if (modelNV != null)
                {
                    //model.pass đã được set new password
                    modelNV.MatKhau = GetPasswordRandom();
                    //db.KhachHang.Update(model);
                    db.Entry(modelNV).State = EntityState.Modified;

                    await db.SaveChangesAsync();

                    #region Send mail
                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("Admin", "hoanglong29032000@gmail.com");
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress("User", modelNV.Email);
                    message.To.Add(to);

                    message.Subject = "Reset Mật khẩu thành công";
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $"<h1>Mật khẩu của bạn đã được reset, mật khẩu mới: {modelNV.MatKhau}  </h1>";
                    bodyBuilder.TextBody = "Mật Khẩu của bạn đã được thay đổi ";
                    message.Body = bodyBuilder.ToMessageBody();
                    // xac thuc email
                    SmtpClient client = new SmtpClient();
                    //connect (smtp address, port , true)
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("hoanglong29032000@gmail.com", "qihdtmmuqslqyaro");
                    //send email
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                    #endregion

                    return View("Index");
                }
                ViewBag.error = "Email không tồn tại trong hệ thống!";
                return View(resetModel);
            }
            return View(resetModel);
        }
        public string GetPasswordRandom()
        {
            Random rnd = new Random();
            string value = "";
            for (int i = 0; i < 6; i++)
            {
                value += rnd.Next(0, 9).ToString();
            }
            return value;
        }
    }
}
