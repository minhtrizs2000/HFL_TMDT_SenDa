using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSenDa.Models
{
    public class CartItem
    {
        public SanPham sanPham { get; set; }
        public int soLuongTon { get; set; }
    }

    public partial class TaiKhoanModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Không được để trống!")]
        public string TenTaiKhoan { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Không được để trống!")]
        public string SoDienThoai { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Không được để trống!")]
        public string CMND { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Không được để trống!")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Không được để trống!")]
        public string MatKhau { get; set; }
        public int LoaiTaiKhoan { get; set; }


        [NotMapped]
        [Compare("MatKhau")]
        [Display(Name = "Nhập Lại Mật Khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }

        //public virtual KhachHang KhachHang { get; set; }
        //public virtual NhanVien NhanVien { get; set; }
    }

    public class ViewModel
    {
        public SanPham sanPham { get; set; }
        public LoaiSanPham loaiSanPham { get; set; }
        public LoaiSenDa loaiSenDa { get; set; }
        public LoaiChauCay loaiChauCay { get; set; }
        public LoaiGiaThe loaiGiaThe { get; set; }
        public KhuyenMai khuyenMai { get; set; }
        public Kho kho { get; set; }
        public NhapKho nhapKho { get; set; }
        public DanhGia danhGia { get; set; }
        public KhachHang khachHang { get; set; }
        public NhanVien nhanVien { get; set; }
        public Quyen quyen { get; set; }
        public LoaiTaiKhoan loaiTaiKhoan { get; set; }
        public DiaChi diaChi { get; set; }
        public DonHang donHang { get; set; }
        public ChiTietDonHang chiTietDonHang { get; set; }

        public IEnumerable<SanPham> ListSanPham { get; set; }
        public IEnumerable<LoaiSanPham> ListLoaiSanPham { get; set; }
        public IEnumerable<LoaiSenDa> ListLoaiSenDa { get; set; }
        public IEnumerable<LoaiChauCay> ListLoaiChauCay { get; set; }
        public IEnumerable<LoaiGiaThe> ListLoaiGiaThe { get; set; }
        public IEnumerable<KhuyenMai> ListKhuyenMai { get; set; }
        public IEnumerable<Kho> ListKho { get; set; }
        public IEnumerable<NhapKho> ListNhapKho { get; set; }
        public IEnumerable<DanhGia> ListDanhGia { get; set; }
        public IEnumerable<KhachHang> ListKhachHang { get; set; }
        public IEnumerable<NhanVien> ListNhanVien { get; set; }
        public IEnumerable<Quyen> ListQuyen { get; set; }
        public IEnumerable<LoaiTaiKhoan> ListLoaiTaiKhoan { get; set; }
        public IEnumerable<DiaChi> ListDiaChi { get; set; }
        public IEnumerable<DonHang> ListDonHang { get; set; }
        public IEnumerable<ChiTietDonHang> ListChiTietDonHang { get; set; }


        public int Year { get; set; }
        public int Month { get; set; }
        public int SLNhap { get; set; }
        public int SLBan { get; set; }
        public decimal TongTienNhap { get; set; }
        public decimal TongTienBan { get; set; }

        List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add_Product_Cart(SanPham sp, int sl = 1)
        {
            var item = Items.FirstOrDefault(s => s.sanPham.IDSanPham == sp.IDSanPham);
            if (item == null)
                items.Add(new CartItem
                {
                    sanPham = sp,
                    soLuongTon = sl
                });
            else
                item.soLuongTon += sl;
        }
        public int Total_quantity()
        {
            return items.Sum(s => s.soLuongTon);
        }
        public decimal Total_money()
        {
            decimal total;
            total = items.Sum(s => ((s.sanPham.Kho.GiaBan - (s.sanPham.Kho.GiaBan * s.sanPham.KhuyenMai.GiaTriKhuyenMai / 100)) * s.soLuongTon) + 20);
            return total;
        }
        public void Update_quantity(int id, int newsl)
        {
            var item = items.Find(s => s.sanPham.IDSanPham == id);

            if (item != null)
            {
                if (items.Find(s => s.sanPham.Kho.SoLuongTon > newsl) != null)
                {
                    item.soLuongTon = newsl;
                }
                else item.soLuongTon = 1;
            }
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s.sanPham.IDSanPham == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }

    }

}